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
      public  List<Project> projects;

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
            Project localProject = new Project(0, name, "null");
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
            //sql = "select id, name, projektfil, type, duration, startdato, slutdato from iteration where projektfil = " + projectID + ";";
            //command = new SQLiteCommand(sql, conn);
            //reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    int ID = Convert.ToInt32(reader["id"]);
            //    string name = (string)(reader["name"]);
            //    int projektfil = (int)(reader["projektfil"]);
            //    string type = (string)(reader["type"]);
            //    int duration = (int)(reader["duration"]);
            //    string startdato = (string)(reader["startdatp"]);
            //    string slutdato = (string)(reader["slutdato"]);
            //    localProject.iterations.Add(new Iteration(ID, name, projektfil, type, duration, startdato, slutdato));
            //}
            //foreach (Iteration iteration in localProject.iterations)
            //{
            //    sql = "select id, name, iteration from graf where iteration = " + iteration.ID + ";";
            //    command = new SQLiteCommand(sql, conn);
            //    reader = command.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        int ID = Convert.ToInt32(reader["id"]);
            //        string name = (string)(reader["name"]);
            //        int iterationID = (int)(reader["iteration"]);
            //        Graph localGraph = new Graph(ID, name, iterationID);
            //        iteration.graphs.Add(localGraph);
            //    }
            //    foreach (Graph graph in iteration.graphs)
            //    {
            //        sql = "select graphID, xValue, yValue from point where graphID = " + graph.ID + ";";
            //        command = new SQLiteCommand(sql, conn);
            //        reader = command.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            int graphID = Convert.ToInt32(reader["graphID"]);
            //            int xValue = (int)(reader["xValue"]);
            //            int yValue = (int)(reader["yValue"]);
            //            graph.MaxXValue++;
            //            graph.pointCollection.Add(new graphPoint(xValue,yValue, graphID));
            //        }
            //    }
            //}
            
        }

        public void LoadIterations(int projectID)
        {
            
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
            foreach (Project project in projects)
            {
                if(project.ID1 == projectID)
                {
                    if(project.iterations.Count > 0)
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
            Iteration localIteration = new Iteration(1,"null", 1,"null",1,"null","null");
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
                    localIteration = new Iteration(ID, name, projectID, type, duration, startdato, slutdato);
                    project.iterations.Add(localIteration);
                }
            }
            
            sql = "insert into graf values(null, 'Business Model'," + localIteration.ID + ")";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "insert into graf values(null, 'Requirements'," + localIteration.ID + ")";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "insert into graf values(null, 'Analyse and Design'," + localIteration.ID + ")";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "insert into graf values(null, 'Implementation'," + localIteration.ID + ")";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "insert into graf values(null, 'Test'," + localIteration.ID + ")";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "insert into graf values(null, 'Deployment'," + localIteration.ID + ")";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "insert into graf values(null, 'Configuration and Change Management'," + localIteration.ID + ")";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "insert into graf values(null, 'Project Management'," + localIteration.ID + ")";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "insert into graf values(null, 'Environment'," + localIteration.ID + ")";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            sql = "Select id, name, iteration from graf where iteration = " + localIteration.ID + "');";
            command = new SQLiteCommand(sql, conn);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                int graphID = Convert.ToInt32(reader["id"]);
                string graphName = (string)(reader["name"]);
                int iterationID = (int)(reader["iteration"]);
                localIteration.graphs.Add(new Graph(graphID, graphName, iterationID));
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
                                String sll = "delete from point where graphid = " + graph.ID + ";";
                                SQLiteCommand comand = new SQLiteCommand(sll, conn);
                                comand.ExecuteNonQuery();
                                iteration.graphs.ToList().Remove(graph);
                            }
                            String sql = "delete from graf where iteration = " + iteration.ID + ";";
                            SQLiteCommand command = new SQLiteCommand(sql, conn);
                            command.ExecuteNonQuery();

                            sql = "delete from iteration where id = " + iterationID + "and projektfil = " + projectID + ";";
                            command = new SQLiteCommand(sql, conn);
                            command.ExecuteNonQuery();
                            project.iterations.ToList().Remove(iteration);
                        }
                    }
                }
            }
        }

        public void SavePoint(int projectID, int graphID, int yValue, int Total)
        {
            foreach (Project project in projects)
            {
                if(project.ID1 == projectID)
                {
                    foreach (Iteration iteration in project.iterations)
                    {
                        foreach (Graph graph in iteration.graphs)
                        {
                            if(graph.ID == graphID)
                            {
                                if(Total > graph.MaxXValue)
                                {
                                    graph.MaxXValue++;
                                    graph.pointCollection.Add(new graphPoint(graph.MaxXValue, yValue, graphID));

                                    String sql = "insert into Point values(" + graphID + ", " + graph.MaxXValue + "," + yValue + ");";
                                    SQLiteCommand command = new SQLiteCommand(sql, conn);
                                    command.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
