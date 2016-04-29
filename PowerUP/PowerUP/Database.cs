using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUP
{
    class Database
    {
        static String connStr = "Data Source=PointsDatabase.db;Version=3";
        SQLiteConnection conn = new SQLiteConnection(connStr);
        public List<Project> projects;
        public Project localProject;
        public Database()
        {
            projects = new List<Project>();
        }

        public bool Connection()
        {
            string curFile = @"PointsDatabase.db";
            if (!File.Exists(curFile))
            {

                SQLiteConnection.CreateFile("PointsDatabase.db");

                conn.Open();
                return false;
            }

            conn.Open();

            return true;

        }

        public void CreateTable()
        {

            String sql = "Create table projektfil (id integer primary key, name string, Description string, startdato string, slutdato string);";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            sql = "Create table iteration (id integer primary key, name string, projektfil integer, foreign key(projektfil) references projektfil(id));";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "alter table iteration add type string;";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "alter table iteration add duration integer;";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "alter table iteration add startdato string;";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "alter table iteration add slutdato string;";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "alter table iteration add internIndex integer;";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            sql = "create table graf (id integer primary key, name string, xValue integer, yValue integer, iteration integer, foreign key (iteration) references iteration(id));";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public void LoadProjects()
        {
            String sql = "select id, name, description from projektfil;";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int ID = Convert.ToInt32(reader["id"]);
                string name = (string)(reader["name"]);
                string description = (string)(reader["description"]);
                projects.Add(new Project(ID, name, description));
            }

        }

        public void Loadprojectfile(string name)
        {
            String sql = "select id, name, description, startdato, slutdato from projektfil where name = '" + name + "';";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int ID = Convert.ToInt32(reader["id"]);
                name = (string)(reader["name"]);
                string description = (string)reader["description"];
                string startdato = (string)reader["startdato"];
                string slutdato = (string)reader["slutdato"];
                foreach (Project project in projects)
                {
                    if (project.Name == name)
                    {
                        projects.ToList().Clear();
                        localProject = new Project(ID, name, description, startdato, slutdato);
                        projects.ToList().Add(localProject);
                    }
                }
            }

        }

        public void LoadIterations(int projectID)
        {
            String sql = "select id, name, projektfil, type, duration, startdato, slutdato, internIndex from iteration where projektfil = " + projectID + ";";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int ID = Convert.ToInt32(reader["id"]);
                string name = (string)(reader["name"]);
                int projektfil = Convert.ToInt32(reader["projektfil"]);
                string type = (string)(reader["type"]);
                int duration = Convert.ToInt32(reader["duration"]);
                string startdato = (string)(reader["startdato"]);
                string slutdato = (string)(reader["slutdato"]);
                int internIndex = Convert.ToInt32(reader["internIndex"]);
                projects[0].iterations.Add(new Iteration(ID, name, projektfil, type, duration, startdato, slutdato, internIndex));
            }
        }

        public void UpdateIteration(int internIndex, string name)
        {
            String sql = "update iteration set internIndex = " + internIndex + " where name = '" + name + "' ;";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public void CreateProject(string name, string description, string startdato, string slutdato)
        {
            String sql = "insert into projektfil values (null,'" + name + "','" + description + "','" + startdato + "','" + slutdato + "');";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "select id from projektfil where description = '" + description + "' and name = '" + name + "';";
            command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int ID = Convert.ToInt32(reader["id"]);
                projects.Add(new Project(ID, name, description, startdato, slutdato));
            }
        }

        public void DeleteProject(int projectID)
        {
            foreach (Project project in projects)
            {
                if (project.ID1 == projectID)
                {
                    if (project.iterations.Count > 0)
                        foreach (Iteration iteration in project.iterations)
                        {
                            foreach (Graph graph in iteration.graphs)
                            {
                                foreach (graphPoint graphpoint in graph.pointCollection)
                                {
                                    graph.pointCollection.ToList().Remove(graphpoint);
                                }
                                String sqll = "delete from point where graphid = " + graph.ID + ";";
                                SQLiteCommand commandd = new SQLiteCommand(sqll, conn);
                                commandd.ExecuteNonQuery();
                                iteration.graphs.ToList().Remove(graph);
                            }
                            String sqlll = "delete from graf where iteration = " + iteration.ID + ";";
                            SQLiteCommand commanddd = new SQLiteCommand(sqlll, conn);
                            commanddd.ExecuteNonQuery();
                            project.iterations.ToList().Remove(iteration);
                        }
                    String sqllll = "delete from iteration where projektfil = " + projectID + ";";
                    SQLiteCommand commandddd = new SQLiteCommand(sqllll, conn);
                    commandddd.ExecuteNonQuery();
                    projects.ToList().Remove(project);
                }
            }
            String sql = "delete from projektfil where id = " + projectID + ";";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public void CreateIteration(int projectID, string name, string type, int duration, string startdato, string slutdato)
        {
            String sql = "insert into iteration values(null,'" + name + "'," + projectID + ",'" + type + "'," + duration + ",'" + startdato + "','" + slutdato + "', 0)";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public void DeleteIteration(int projectID, int iterationID)
        {
            foreach (Project project in projects)
            {
                if (project.ID1 == projectID)
                {
                    foreach (Iteration iteration in project.iterations)
                    {
                        if (iteration.ID == iterationID)
                        {
                            foreach (Graph graph in iteration.graphs)
                            {
                                foreach (graphPoint graphpoint in graph.pointCollection)
                                {

                                    graph.pointCollection.ToList().Remove(graphpoint);
                                }
                                String sll = "delete from point where graphid = " + graph.ID + ";";
                                SQLiteCommand comand = new SQLiteCommand(sll, conn);
                                comand.ExecuteNonQuery();
                                iteration.graphs.ToList().Remove(graph);
                            }
                            String sql = "delete from graf where iteration = " + iteration.ID + ";";
                            SQLiteCommand command = new SQLiteCommand(sql, conn);
                            command.ExecuteNonQuery();

                            sql = "delete from iteration where id = " + iterationID + " and projektfil = " + projectID + ";";
                            command = new SQLiteCommand(sql, conn);
                            command.ExecuteNonQuery();
                            project.iterations.ToList().Remove(iteration);
                        }
                    }
                }
            }
        }

        public void SavePoint(int yValue, int iterationID, string graphType, int maxXValue)
        {
            String sql = "insert into graf values (null, '" + graphType + "', " + maxXValue + "," + yValue + "," + iterationID + ");";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public int GetIterationID(string name)
        {
            int tempID = 0;
            String sql = "select id from iteration where name = '" + name + "';";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tempID = Convert.ToInt32(reader["id"]);
            }
            return tempID;
        }

        public int GetDuration(string name)
        {
            int duration = 0;
            String sql = "select duration from iteration where name = '" + name + "';";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                duration = Convert.ToInt32(reader["duration"]);
            }
            return duration;
        }

        public List<int> GetGraphPoints(string graphType, int iterationID)
        {
            List<int> datalist = new List<int>();
            String sql = "select yValue from Graf where name = '" + graphType + "' and iteration = "+ iterationID + ";";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int yValue = Convert.ToInt32(reader["yValue"]);
                datalist.Add(yValue);
            }
            return datalist;
        }

        public void ClearGraph(string graphType, int iterationID)
        {
            String sql = "Delete from Graf where name = '" + graphType + "' and iteration = " + iterationID + ";";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }
    }
}
