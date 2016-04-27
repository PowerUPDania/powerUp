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
        List<Project> projects;

        public Database()
        {
            projects = new List<Project>();
            Connection();
            CreateTable();
        }

        public void Connection()
        {
            string curFile = @"PointsDatabase.db";
            if (!File.Exists(curFile))
            {
                SQLiteConnection.CreateFile("PointsDatabase.db");
            }

            conn.Open();

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

            sql = "create table graf (id integer primary key, name string, iteration integer, foreign key (iteration) references iteration(id));";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            sql = "create table Point (graphID integer, foreign key (graphID) references graf(id));";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "alter table point add xValue integer;";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "alter table point add yValue integer;";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public void LoadProjects()
        {


            String sql = "select id, name from projektfil;";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int ID = Convert.ToInt32(reader["id"]);
                string name = (string)(reader["name"]);
                projects.Add(new Project(ID, name));
            }

        }

        public void Loadprojectfile(int projectID)
        {
            Project localProject = new Project(projectID, "null");
            String sql = "select id, name, description, startdato, slutdato from projekfil where id = " + projectID + ";";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int ID = Convert.ToInt32(reader["id"]);
                string name = (string)(reader["name"]);
                string description = (string)reader["description"];
                string startdato = (string)reader["startdato"];
                string slutdato = (string)reader["slutdato"];
                foreach (Project project in projects)
                {
                    if (project.ID1 == ID)
                    {
                        projects.ToList().Remove(project);
                        localProject = new Project(ID, name, description, startdato, slutdato);
                        projects.ToList().Add(localProject);
                    }
                }
            }
            sql = "select id, name, projektfil, type, duration, startdato, slutdato from iteration where projektfil = " + projectID + ";";
            command = new SQLiteCommand(sql, conn);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                int ID = Convert.ToInt32(reader["id"]);
                string name = (string)(reader["name"]);
                int projektfil = (int)(reader["projektfil"]);
                string type = (string)(reader["type"]);
                int duration = (int)(reader["duration"]);
                string startdato = (string)(reader["startdatp"]);
                string slutdato = (string)(reader["slutdato"]);
                localProject.iterations.Add(new Iteration(ID, name, projektfil, type, duration, startdato, slutdato));
            }
            foreach (Iteration iteration in localProject.iterations)
            {
                sql = "select id, name, iteration from graf where iteration = " + iteration.ID + ";";
                command = new SQLiteCommand(sql, conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int ID = Convert.ToInt32(reader["id"]);
                    string name = (string)(reader["name"]);
                    int iterationID = (int)(reader["iteration"]);
                    iteration.graphs.Add(new Graph(ID, name, iterationID));
                }

            }
        }

        public void CreateProject(string name, string description, string startdato, string slutdato)
        {
            String sql = "insert into projektfil values (null,'" + name + "','" + description + "','" + startdato + "','" + slutdato + "' );";
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
            List<int> deletion = new List<int>();
            String sql = "Select id from iteration where projektfil = " + projectID + ");";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                deletion.Add((int)(reader["id"]));
            }
            foreach (int IterationID in deletion)
            {
                sql = "delete id, name, iteration from graf where iteration = " + IterationID + ");";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
            }
            sql = "delete id, name, projektfil, type, duration, startdato, slutdato from iteration where projektfil = " + projectID + ");";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "delete id, name, Description, startdato, slutdato from projektfil where id = " + projectID + ");";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public void CreateIteration(int projectID, string name, string type, int duration, string startdato, string slutdato)
        {
            String sql = "insert into iteration values(null,'" + name + "'," + projectID + ",'" + type + "'," + duration + ",'" + startdato + "','" + slutdato + "')";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            sql = "Select id from iteration where projektfil = " + projectID + " and name = '" + name + "');";
            command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            int ID = 10000;
            while (reader.Read())
            {
                ID = Convert.ToInt32(reader["id"]);
            }
            foreach (Project project in projects)
            {
                if (project.ID1 == projectID)
                {
                    project.iterations.Add(new Iteration(ID, name, projectID, type, duration, startdato, slutdato));
                }
            }
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
                                String sll = "delete graphid, xValue, yValue from point where graphid = " + graph.ID + ";";
                                SQLiteCommand comand = new SQLiteCommand(sll, conn);
                                comand.ExecuteNonQuery();
                                iteration.graphs.ToList().Remove(graph);
                            }
                            String sql = "delete id, name, iteration from graf where iteration = " + iteration.ID + ";";
                            SQLiteCommand command = new SQLiteCommand(sql, conn);
                            command.ExecuteNonQuery();

                            sql = "delete id, name, projektfil, type,  duration, startdato, slutdato from iteration where id = " + iterationID + "and projektfil = " + projectID + ";";
                            command = new SQLiteCommand(sql, conn);
                            command.ExecuteNonQuery();
                            project.iterations.ToList().Remove(iteration);
                        }
                    }
                }
            }
        }
    }
}
