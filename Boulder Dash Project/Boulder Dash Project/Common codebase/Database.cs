using System;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;
namespace Boulder_Dash_Project
{
    class Database
    {
        public static string s = Path.GetFullPath("Results.mdf");
        public static string ConnStr = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =" + s + "; Integrated Security = True";
        public static List<int> ids = new List<int>();
        public static List<string> names = new List<string>();
        public static List<int> scores = new List<int>();
        public static List<int> steps = new List<int>();
        public static List<int> digs = new List<int>();
        public static List<int> lives = new List<int>();
        public static List<string> times = new List<string>();
        public static List<int> RockDowns = new List<int>();
        public static List<int> RockMoved = new List<int>();
        public static List<string> Results = new List<string>();


        public static void GetResults()
        {
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();
                string sql = "SELECT * FROM Players";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int score = reader.GetInt32(2);
                    int step = reader.GetInt32(3);
                    int dig = reader.GetInt32(4);
                    int live = reader.GetInt32(5);
                    string time = reader.GetString(6);
                    int rockdown = reader.GetInt32(7);
                    int rockmoved = reader.GetInt32(8);
                    string result = reader.GetString(9);

                    ids.Add(id);
                    names.Add(name);
                    scores.Add(score);
                    steps.Add(step);
                    digs.Add(dig);
                    lives.Add(live);
                    times.Add(time);
                    RockDowns.Add(rockdown);
                    RockMoved.Add(rockmoved);
                    Results.Add(result);
                }
            }

            string writePath = "allresult.txt";

            using (StreamWriter sw = new StreamWriter(writePath))
            {
                for (int i = 0; i < names.Count; i++)
                {
                    sw.WriteLine("Id: " + ids[i]);
                    sw.WriteLine("Name: " + names[i]);
                    sw.WriteLine("Result: " + scores[i]);
                    sw.WriteLine("Steps: " + steps[i]);
                    sw.WriteLine("Digs: " + digs[i]);
                    sw.WriteLine("Lives at the end: " + lives[i]);
                    sw.WriteLine("Time: " + times[i]);
                    sw.WriteLine("Rock down by gravity: " + RockDowns[i]);
                    sw.WriteLine("Rock moved by hero: " + RockMoved[i]);
                    sw.WriteLine("Result: " + Results[i]);
                    sw.WriteLine("");
                }
            }
            Process.Start(new ProcessStartInfo(@"allresult.txt") { UseShellExecute = true });

            names.Clear();
            scores.Clear();

        }

        public static void GetBestResults()
        {
            string writePath = "bestresult.txt";

            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();
                string sql = "SELECT* FROM Players ORDER BY Score DESC";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string name = reader.GetString(1);
                    int score = reader.GetInt32(2);

                    names.Add(name);
                    scores.Add(score);
                }
            }

            using (StreamWriter sw = new StreamWriter(writePath))
            {
                for (int i = 0; i < names.Count; i++)
                {
                    sw.WriteLine("Name: " + names[i] + "    " + "Result: " + scores[i]);
                }
            }
            Process.Start(new ProcessStartInfo(@"bestresult.txt") { UseShellExecute = true });
        }

        public static void EndLevel(string result)
        {
            Output.Clear();
            Field.frame.Clear();
            Levels.GetArrayFromFile("empty.txt");

            string name = Output.GetName();

            string writePath = "result.txt";

            using (StreamWriter sw = new StreamWriter(writePath))
            {
                sw.WriteLine("Name: " + name);
                sw.WriteLine("Result: " + result);
                sw.WriteLine("Score: " + GameField.score);
                sw.WriteLine("Steps: " + Hero.steps);
                sw.WriteLine("Digs: " + Hero.digs);
                sw.WriteLine("Lives at the end: " + Hero.lives);
                sw.WriteLine("Time: " + DateTime.Now.Subtract(GameField.Time));
                sw.WriteLine("Rock down by gravity: " + Rock.RocksDownGravity);
                sw.WriteLine("Rock moved by hero: " + Hero.RocksMoveByHero);
                sw.WriteLine("Result: " + result);
                sw.Close();
            }
            GameField.score = GameField.maxpoint;
            Process.Start(new ProcessStartInfo(@"result.txt") { UseShellExecute = true });

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnStr))
                using (var cmd1 = new SqlDataAdapter())
                {
                    connection.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = connection;
                    //cmd.CommandText = "INSERT INTO Players (Name, Score, Steps, Digs, Livesend) VALUES ('" + name+"', "+score+", "+Hero.steps+ ", " + Hero.digs + ", " + Hero.lives + ")";
                    //cmd.CommandText = "INSERT INTO Players (Name, Score) VALUES ('" + name + "', " + score + ")";
                    string time = Convert.ToString(DateTime.Now - GameField.Time);
                    cmd.CommandText = "INSERT INTO Players (Name, Score, Steps, Digs, Livesend, Time, RockDown, RockMoveByHero, Result) VALUES ('" + name + "', " + GameField.score + ", " + Hero.steps + ", " + Hero.digs + ", " + Hero.lives + ",'" + time + "'" + ", " + Rock.RocksDownGravity + ", " + Hero.RocksMoveByHero + ", " + "'" + result + "')";
                    cmd.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                Output.Exception(ex);
            }

        }
    }
}
