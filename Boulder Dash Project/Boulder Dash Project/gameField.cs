using System;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Media;
using System.Drawing;

namespace Boulder_Dash_Project
{
    class GameField : Field
    {
        public static string s = Path.GetFullPath("Results.mdf");
        public static string ConnStr = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename ="+s+"; Integrated Security = True";
        public static bool Failedload=false;
        public static int score = 0;
        public static int maxpoint = 0;
        public static System.DateTime Time = DateTime.Now;
        public static bool TechnicalLevel = false;

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

        public static bool win = false;

        public static void Win()
        {
            player.SoundLocation = "win.wav";
            player.Play();
            
            Process.Start(new ProcessStartInfo(@"win.mp4") { UseShellExecute = true });

            Console.Clear();
            Console.WriteLine("Win!");
            Thread.Sleep(5000);
            player.Stop();
            score = maxpoint;
            Console.Clear();

            win = true;
        }

        public static void Defeat()
        {
            player.SoundLocation = "loose.wav";
            player.Play();
            Process.Start(new ProcessStartInfo(@"defeat.mp4") { UseShellExecute = true });

            Console.Clear();
            Console.WriteLine("Defeat!");
            Thread.Sleep(3000);
            player.Stop();
            
            Console.Clear();
            //EndLevel("Defeat");

            win = false;
        }

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
                    int step  = reader.GetInt32(3);
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
            Console.Clear();
            Field.frame.Clear();
            GameField.GetArrayFromFile("empty.txt");
            GameField.Renderer();
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("Enter name");
            Console.SetCursorPosition(1, 2);
            string name = Console.ReadLine();
            Console.WriteLine("Thank you! Saving the result. Please wait...");
            string writePath = "result.txt";

            using (StreamWriter sw = new StreamWriter(writePath))
            {
                sw.WriteLine("Name: " + name);
                sw.WriteLine("Result: " + result);
                sw.WriteLine("Score: " + score);
                sw.WriteLine("Steps: " + Hero.steps);
                sw.WriteLine("Digs: " + Hero.digs);
                sw.WriteLine("Lives at the end: " + Hero.lives);
                sw.WriteLine("Time: " + DateTime.Now.Subtract(GameField.Time));
                sw.WriteLine("Rock down by gravity: " + Rock.RocksDownGravity);
                sw.WriteLine("Rock moved by hero: " + Hero.RocksMoveByHero);
                sw.WriteLine("Result: " + result);
                sw.Close();
            }
            score = maxpoint;
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
                    string time = Convert.ToString(DateTime.Now - Time);
                    cmd.CommandText = "INSERT INTO Players (Name, Score, Steps, Digs, Livesend, Time, RockDown, RockMoveByHero, Result) VALUES ('" + name + "', " + score + ", " + Hero.steps + ", " + Hero.digs + ", " + Hero.lives +",'" + time + "'"+ ", " + Rock.RocksDownGravity + ", " + Hero.RocksMoveByHero + ", " +  "'"+result+"')";
                    cmd.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void GetArrayFromFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            int rowCount = lines.Length;
            int SizeOfLine = lines[0].Length;
            Failedload = false;
            for (int i = 0; i < rowCount; i++)
            {
                char[] line = lines[i].ToCharArray();
                if (line.Length != SizeOfLine)
                {
                    Failedload=true;
                    break;
                }
                string[] strline = new string[line.Length];
                for (int k = 0; k < line.Length; k++)
                {
                    strline[k] = Convert.ToString(line[k]);
                    if (strline[k] == Diamond.value)
                    {
                        GameField.maxpoint = GameField.maxpoint + 100;
                    }
                }
                frame.Add(strline);
            }
        }

        public static void SaveToFile()
        {
            string writePath = "save.txt";

            using (StreamWriter sw = new StreamWriter(writePath))
            {
                for (int j = 0; j < Field.frame[0].Length; j++)
                {
                    sw.Write(String.Format("{0}", "-"));
                }
                sw.WriteLine("");
                for (int i = 0; i < Field.frame.Count; i++)
                {
                    for (int j = 0; j < Field.frame[0].Length; j++)
                    {
                        sw.Write(String.Format("{0}", Field.frame[i][j]));
                    }
                    sw.WriteLine("");
                }
                sw.Close();
            }

        }

        public static void GravityFunction()
        {
            while (true)
            {
                try
                {
                    if (Rock.CountRock() == 1)
                    {
                        Rock.MoveRock1();
                    }
                    else if (Rock.CountRock() > 1)
                    {

                        Rock.MoveRock1();
                        for (int i = 0; i < 5; i++)
                            Rock.MoveRock2();

                    }
                    Thread.Sleep(200);
                }
                catch { }
            }
        }
        public static void LivesFunction()
        {
            while (true)
            {
                try
                {
                    for (int i = 0; i <= Field.frame.Count - 1; i++)
                    {
                        for (int x = 0; x <= Field.frame[i].Length - 1; x++)
                        {
                            if (Field.frame[i][x] == Rock.value)
                            {
                                try
                                {
                                    if (Field.frame[i + 1][x] == Hero.value)
                                    {
                                        Hero.lives = Hero.lives - 1;
                                        Console.SetCursorPosition(1, 26);
                                        Console.Write("Lives: " + Hero.lives + "  ");
                                        if (Hero.lives == 0)
                                        {
                                            GameField.Defeat();
                                            Field.frame[i][x] = Empty.value;
                                            Field.frame[i + 1][x] = Rock.value;
                                            Console.SetCursorPosition(x, i);
                                            Console.Write(Empty.value);
                                            Console.SetCursorPosition(x, i + 1);
                                            Console.Write(Rock.value);
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }
                catch { }
                Thread.Sleep(200);
            }

        }
        public static void Renderer()
        {
            Console.Clear();
            for (int i = 0; i < Field.frame.Count; i++)
            {
                Console.WriteLine(string.Join("", Field.frame[i]));
            }
        }

        
        public static void AddScores()
        {
            GameField.score += 100;
            if (GameField.score >= GameField.maxpoint && TechnicalLevel == false) GameField.Win();
            Console.SetCursorPosition(1, 27);
            Console.Write("Score: " + GameField.score);
        }
        

    }
}