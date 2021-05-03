using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Media;
using NAudio.Wave;



namespace Boulder_Dash_Project
{
    class Program
    {
        class Field
        {

            public static List<string[]> frame = new List<string[]>();
            public static bool gameStatus
            {
                get
                {
                    return gameStatus;
                }
                set
                {
                    gameStatus = value;
                }
            }
            public static int score
            {
                get
                {
                    return score;
                }
                set
                {
                    score = value;
                }
            }

            public static string hero
            {
                get
                {
                    return hero;
                }
                set
                {
                    hero = value;
                }
            }
            public static string rock
            {
                get
                {
                    return rock;
                }
                set
                {
                    rock = value;
                }
            }
            public static string diamond
            {
                get
                {
                    return diamond;
                }
                set
                {
                    diamond = value;
                }
            }
            public static string sand
            {
                get
                {
                    return sand;
                }
                set
                {
                    sand = value;
                }
            }
            public static string empty
            {
                get
                {
                    return empty;
                }
                set
                {
                    empty = value;
                }
            }
            public static int lives
            {
                get
                {
                    return lives;
                }
                set
                {
                    lives = value;
                }
            }
            static public SoundPlayer player = new SoundPlayer();

        }


        class gameField : Field
        {
            public static List<string[]> frame = new List<string[]>();
            public static bool gameStatus = true;
            public static Random rnd = new Random();
            public static int score = 0;
            public static int maxpoint = 0;
            public static string hero = "I";
            public static string rock = "o";
            public static string diamond = "@";
            public static string sand = "*";
            public static string empty = " ";
            public static int lives = 500;
            public static bool BFS_res = false;
            public static int BFS_score;
            static public SoundPlayer player = new SoundPlayer();
            public static List<int> BFS_x = new List<int>();
            public static List<int> BFS_y = new List<int>();
            public static List<int> BFS_x_help = new List<int>();
            public static List<int> BFS_y_help = new List<int>();
            public static bool Deadlock = false;
            public static int x;
            public static int y;
            public static int distance;


            public static void MusicFunction()
            {
                
                int i, time;
                TimeSpan totalTime;
                    while (true)
                    {
                        i = rnd.Next() % 23 + 1;
                        var reader = new Mp3FileReader("music1.mp3");
                        var waveOut = new WaveOut();
                        switch (i)
                        {
                            case 1:
                                reader = new Mp3FileReader("music1.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                    time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                            case 2:
                                reader = new Mp3FileReader("music2.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                            case 3:
                                reader = new Mp3FileReader("music3.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                    time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                            case 4:
                                reader = new Mp3FileReader("music4.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                    time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                            case 5:
                                reader = new Mp3FileReader("music5.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                    time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                            case 6:
                                reader = new Mp3FileReader("music6.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                    time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                            case 7:
                                reader = new Mp3FileReader("music7.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                    time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                            case 8:
                                reader = new Mp3FileReader("music8.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                    time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                            case 9:
                                reader = new Mp3FileReader("music9.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                    time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                            case 10:
                                reader = new Mp3FileReader("music10.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                    time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                            case 11:
                                reader = new Mp3FileReader("music11.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                    time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                            case 12:
                                reader = new Mp3FileReader("music12.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                    time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                            case 13:
                                reader = new Mp3FileReader("music13.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                    time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                            case 14:
                                reader = new Mp3FileReader("music14.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                    time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                            case 15:
                                reader = new Mp3FileReader("music15.mp3");
                                totalTime = reader.TotalTime;
                                waveOut.Init(reader);
                                waveOut.Play();
                                time = totalTime.Minutes * 60 + totalTime.Seconds;
                                while (time > 0)
                                {
                                    var key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                    else if (key.Key == ConsoleKey.R)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                    }
                                    else if (key.Key == ConsoleKey.F)
                                    {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                    }
                                    time = time - 2;
                                    Thread.Sleep(2000);
                                }
                                waveOut.Stop();
                                break;
                        case 16:
                            reader = new Mp3FileReader("music16.mp3");
                            totalTime = reader.TotalTime;
                            waveOut.Init(reader);
                            waveOut.Play();
                            time = totalTime.Minutes * 60 + totalTime.Seconds;
                            while (time > 0)
                            {
                                var key = Console.ReadKey();
                                if (key.Key == ConsoleKey.N)
                                {
                                    break;
                                }
                                else if (key.Key == ConsoleKey.R)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                }
                                else if (key.Key == ConsoleKey.F)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                }
                                time = time - 2;
                                Thread.Sleep(2000);
                            }
                            waveOut.Stop();
                            break;
                        case 17:
                            reader = new Mp3FileReader("music17.mp3");
                            totalTime = reader.TotalTime;
                            waveOut.Init(reader);
                            waveOut.Play();
                            time = totalTime.Minutes * 60 + totalTime.Seconds;
                            while (time > 0)
                            {
                                var key = Console.ReadKey();
                                if (key.Key == ConsoleKey.N)
                                {
                                    break;
                                }
                                else if (key.Key == ConsoleKey.R)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                }
                                else if (key.Key == ConsoleKey.F)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                }
                                time = time - 2;
                                Thread.Sleep(2000);
                            }
                            waveOut.Stop();
                            break;
                        case 18:
                            reader = new Mp3FileReader("music18.mp3");
                            totalTime = reader.TotalTime;
                            waveOut.Init(reader);
                            waveOut.Play();
                            time = totalTime.Minutes * 60 + totalTime.Seconds;
                            while (time > 0)
                            {
                                var key = Console.ReadKey();
                                if (key.Key == ConsoleKey.N)
                                {
                                    break;
                                }
                                else if (key.Key == ConsoleKey.R)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                }
                                else if (key.Key == ConsoleKey.F)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                }
                                time = time - 2;
                                Thread.Sleep(2000);
                            }
                            waveOut.Stop();
                            break;
                        case 19:
                            reader = new Mp3FileReader("music19.mp3");
                            totalTime = reader.TotalTime;
                            waveOut.Init(reader);
                            waveOut.Play();
                            time = totalTime.Minutes * 60 + totalTime.Seconds;
                            while (time > 0)
                            {
                                var key = Console.ReadKey();
                                if (key.Key == ConsoleKey.N)
                                {
                                    break;
                                }
                                else if (key.Key == ConsoleKey.R)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                }
                                else if (key.Key == ConsoleKey.F)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                }
                                time = time - 2;
                                Thread.Sleep(2000);
                            }
                            waveOut.Stop();
                            break;
                        case 20:
                            reader = new Mp3FileReader("music20.mp3");
                            totalTime = reader.TotalTime;
                            waveOut.Init(reader);
                            waveOut.Play();
                            time = totalTime.Minutes * 60 + totalTime.Seconds;
                            while (time > 0)
                            {
                                var key = Console.ReadKey();
                                if (key.Key == ConsoleKey.N)
                                {
                                    break;
                                }
                                else if (key.Key == ConsoleKey.R)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                }
                                else if (key.Key == ConsoleKey.F)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                }
                                time = time - 2;
                                Thread.Sleep(2000);
                            }
                            waveOut.Stop();
                            break;
                        case 21:
                            reader = new Mp3FileReader("music21.mp3");
                            totalTime = reader.TotalTime;
                            waveOut.Init(reader);
                            waveOut.Play();
                            time = totalTime.Minutes * 60 + totalTime.Seconds;
                            while (time > 0)
                            {
                                var key = Console.ReadKey();
                                if (key.Key == ConsoleKey.N)
                                {
                                    break;
                                }
                                else if (key.Key == ConsoleKey.R)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                }
                                else if (key.Key == ConsoleKey.F)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                }
                                time = time - 2;
                                Thread.Sleep(2000);
                            }
                            waveOut.Stop();
                            break;
                        case 22:
                            reader = new Mp3FileReader("music22.mp3");
                            totalTime = reader.TotalTime;
                            waveOut.Init(reader);
                            waveOut.Play();
                            time = totalTime.Minutes * 60 + totalTime.Seconds;
                            while (time > 0)
                            {
                                var key = Console.ReadKey();
                                if (key.Key == ConsoleKey.N)
                                {
                                    break;
                                }
                                else if (key.Key == ConsoleKey.R)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                }
                                else if (key.Key == ConsoleKey.F)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                }
                                time = time - 2;
                                Thread.Sleep(2000);
                            }
                            waveOut.Stop();
                            break;
                        case 23:
                            reader = new Mp3FileReader("music23.mp3");
                            totalTime = reader.TotalTime;
                            waveOut.Init(reader);
                            waveOut.Play();
                            time = totalTime.Minutes * 60 + totalTime.Seconds;
                            while (time > 0)
                            {
                                var key = Console.ReadKey();
                                if (key.Key == ConsoleKey.N)
                                {
                                    break;
                                }
                                else if (key.Key == ConsoleKey.R)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Min(waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                                }
                                else if (key.Key == ConsoleKey.F)
                                {
                                    waveOut.Volume = Convert.ToSingle(Math.Max(waveOut.Volume - Convert.ToSingle(0.1), 0));
                                }
                                time = time - 2;
                                Thread.Sleep(2000);
                            }
                            waveOut.Stop();
                            break;
                    }

                    
                    
                }
            }

            public static void Win()
            {
                
                Field.player.SoundLocation = "win.wav";
                Field.player.Play();

                Console.Clear();
                Console.WriteLine("Win!");
                Thread.Sleep(5000);
                Field.player.Stop();
                gameField.score = gameField.maxpoint;

                Console.Clear();
                
            }

            public static void Defeat()
            {
                Console.Clear();
                Field.player.Stop();
                Field.player.SoundLocation = "loose.wav";
                Field.player.Play();
                
                Console.WriteLine("Defeat!");
                Thread.Sleep(3000);
                Field.player.Stop();
                gameField.score = gameField.maxpoint;
            }

            public static void GetArrayFromFile(string fileName)
            {
                string[] lines = File.ReadAllLines(fileName);
                int rowCount = lines.Length;

                for (int i = 0; i < rowCount - 1; i++)
                {
                    char[] line = lines[i + 1].ToCharArray();
                    string[] strline = new string[line.Length];
                    for (int k = 0; k < line.Length; k++)
                        strline[k] = Convert.ToString(line[k]);
                    Field.frame.Add(strline);
                }
            }
            
            public static void GetResFromFile(string fileName)
            {
                string[] lines = File.ReadAllLines(fileName);
                int rowCount = lines.Length;

                for (int i = 0; i < rowCount - 1; i++)
                {
                    char[] line = lines[i + 1].ToCharArray();
                    string[] strline = new string[line.Length];
                    for (int k = 0; k < line.Length; k++)
                        strline[k] = Convert.ToString(line[k]);
                    Console.WriteLine(strline);
                }
            }

            public static void ThreadFunction()
            {
                
                
                    while (gameField.gameStatus == true)
                    {
                        Console.SetCursorPosition(Field.frame[1].Length, Field.frame.Count);
                        if (CountRock() == 1)
                        {
                            MoveRock1();
                            
                        }
                        else if (CountRock() > 1)
                        {
                            
                                MoveRock1();
                                for (int i = 0; i < 5; i++)
                                    MoveRock2();
                            
                        }
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
                //Console.WriteLine(BFS_res);
            }

            public static void MoveHero(ConsoleKeyInfo keyInfo)
            {
                bool stat = true;
                
                
                    for (int i = Field.frame.Count - 1; i >= 0; i--)
                    {
                        for (int x = 0; x < Field.frame[i].Length; x++)
                        {
                            if (Field.frame[i][x] == gameField.hero)
                            {  
                            if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
                                {
                                    if (stat)
                                    {
                                        if ((i - 1) >= 0 && Field.frame[i - 1][x] == gameField.diamond)
                                        {
                                            CollectUp(ref i, ref x);
                                            stat = false;
                                        }
                                        else
                                        {
                                            GoUp(ref i, ref x, ref stat);
                                        }
                                    }

                                }
                                if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
                                {
                                    if (stat) GoDown(ref i, ref x, ref stat);
                                    CollectDown(ref i, ref x);
                                }
                                if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
                                {
                                    if (stat) GoLeft(ref i, ref x, ref stat);
                                    CollectLeft(ref i, ref x);
                                }
                                if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
                                {
                                    if (stat) GoRight(ref i, ref x, ref stat);
                                    CollectRight(ref i, ref x);
                                }
                                if (keyInfo.Key == ConsoleKey.E)
                                {
                                    if (stat) GoDig(ref i, ref x, ref stat);
                                }
                                if (keyInfo.Key == ConsoleKey.Q)
                                {
                                    if (stat) GoDigL(ref i, ref x, ref stat);
                                }
                                if (keyInfo.Key == ConsoleKey.L)
                                {
                                    gameField.score = gameField.maxpoint;
                                    gameField.Defeat();
                                }
                                if (keyInfo.Key == ConsoleKey.K)
                                {
                                    gameField.Win();
                                }
                            break;
                            }

                        }
                    }
                

            }
            public static void AddScores()
            {
                gameField.score += 100;
                if (gameField.score >= gameField.maxpoint) gameField.Win();
                Console.SetCursorPosition(12, 24);
                Console.Write("Score: " + gameField.score);
            }
            public static void GoUp(ref int i, ref int x, ref bool stat)
            {
                try
                {
                    if ((i - 1) >= 0 && Field.frame[i - 1][x] == gameField.sand || Field.frame[i - 1][x] == gameField.empty)
                    {
                        Field.frame[i][x] = gameField.empty;
                        Field.frame[i - 1][x] = gameField.hero;
                        Console.SetCursorPosition(x, i);
                        Console.Write(gameField.empty);
                        Console.SetCursorPosition(x, i - 1);
                        Console.Write(gameField.hero);
                        stat = false;
                        gameField.x = x;
                        gameField.y = i - 1;
                        Console.SetCursorPosition(40, 24);
                        Console.Write("Coordinates: x=" + gameField.x + ", y=" + gameField.y + " ");
                    }
                }
                catch { }
            }
            public static void GoDown(ref int i, ref int x, ref bool stat)
            {
                if ((i + 1) <= (Field.frame.Count - 1) && Field.frame[i + 1][x] == gameField.sand || Field.frame[i + 1][x] == gameField.empty)
                {
                    Field.frame[i][x] = gameField.empty;
                    Field.frame[i + 1][x] = gameField.hero;
                    Console.SetCursorPosition(x, i);
                    Console.Write(gameField.empty);
                    Console.SetCursorPosition(x, i + 1);
                    Console.Write(gameField.hero);
                    stat = false;
                    gameField.x = x;
                    gameField.y = i + 1;
                    Console.SetCursorPosition(40, 24);
                    Console.Write("Coordinates: x=" + gameField.x + ", y=" + gameField.y + " ");
                }
                
            }
            public static void GoLeft(ref int i, ref int x, ref bool stat)
            {

                if ((x - 1) >= 0 && Field.frame[i][x - 1] == gameField.sand || Field.frame[i][x - 1] == gameField.empty)
                {
                    Field.frame[i][x] = gameField.empty;
                    Field.frame[i][x - 1] = gameField.hero;
                    Console.SetCursorPosition(x, i);
                    Console.Write(gameField.empty);
                    Console.SetCursorPosition(x - 1, i);
                    Console.Write(gameField.hero);
                    stat = false;
                    gameField.x = x - 1;
                    gameField.y = i;
                    Console.SetCursorPosition(40, 24);
                    Console.Write("Coordinates: x=" + gameField.x + ", y=" + gameField.y + " ");
                }
                else if ((x - 2) >= 0 && Field.frame[i][x - 2] == gameField.empty && Field.frame[i][x - 1] == gameField.rock)
                {
                    Field.frame[i][x] = gameField.empty;
                    Field.frame[i][x - 1] = gameField.hero;
                    Field.frame[i][x - 2] = gameField.rock;
                    Console.SetCursorPosition(x, i);
                    Console.Write(gameField.empty);
                    Console.SetCursorPosition(x, i - 1);
                    Console.Write(gameField.empty);
                    Console.SetCursorPosition(x - 1, i);
                    Console.Write(gameField.hero);
                    Console.SetCursorPosition(x - 2, i);
                    Console.Write(gameField.rock);
                    stat = false;
                    gameField.x = x - 1;
                    gameField.y = i;
                    Console.SetCursorPosition(40, 24);
                    Console.Write("Coordinates: x=" + gameField.x + ", y=" + gameField.y + " ");
                }
                
            }
            public static void GoRight(ref int i, ref int x, ref bool stat)
            {
                if ((x - 1) <= (Field.frame[i].Length - 1) && Field.frame[i][x + 1] == gameField.sand || Field.frame[i][x + 1] == gameField.empty)
                {

                    Field.frame[i][x] = gameField.empty;
                    Field.frame[i][x + 1] = gameField.hero;
                    Console.SetCursorPosition(x, i);
                    Console.Write(gameField.empty);
                    Console.SetCursorPosition(x + 1, i);
                    Console.Write(gameField.hero);
                    stat = false;
                    gameField.x = x + 1;
                    gameField.y = i;
                    Console.SetCursorPosition(40, 24);
                    Console.Write("Coordinates: x=" + gameField.x + ", y=" + gameField.y + " ");
                }
                else if ((x - 2) <= (Field.frame[i].Length - 1) && Field.frame[i][x + 1] == gameField.rock && Field.frame[i][x + 2] == gameField.empty)
                {
                    Field.frame[i][x] = gameField.empty;
                    Field.frame[i][x + 1] = gameField.hero;
                    Field.frame[i][x + 2] = gameField.rock;
                    Console.SetCursorPosition(x, i);
                    Console.Write(gameField.empty);
                    Console.SetCursorPosition(x + 1, i);
                    Console.Write(gameField.hero);
                    Console.SetCursorPosition(x + 2, i);
                    Console.Write(gameField.rock);
                    stat = false;
                    gameField.x = x + 1;
                    gameField.y = i;
                    Console.SetCursorPosition(40, 24);
                    Console.Write("Coordinates: x=" + gameField.x + ", y=" + gameField.y + " ");
                }
                
            }
            public static void GoDig(ref int i, ref int x, ref bool stat)
            {
                if ((x - 1) <= (Field.frame[i].Length - 1) && Field.frame[i][x + 1] == gameField.sand)
                {
                    Field.frame[i][x + 1] = gameField.empty;
                    Console.SetCursorPosition(x + 1, i);
                    Console.Write(gameField.empty);
                    stat = false;
                }
            }
            public static void GoDigL(ref int i, ref int x, ref bool stat)
            {
                if ((x - 1) >= 0 && Field.frame[i][x - 1] == gameField.sand)
                {
                    Field.frame[i][x - 1] = gameField.empty;
                    Console.SetCursorPosition(x - 1, i);
                    Console.Write(gameField.empty);
                    stat = false;
                }
            }
            public static void CollectUp(ref int i, ref int x)
            {
                if ((i - 1) >= 0 && Field.frame[i - 1][x] == gameField.diamond)
                {
                    Field.frame[i][x] = gameField.empty;
                    Field.frame[i - 1][x] = gameField.hero;
                    Console.SetCursorPosition(x, i);
                    Console.Write(gameField.empty);
                    Console.SetCursorPosition(x, i - 1);
                    Console.Write(gameField.hero);
                    AddScores();
                }
            }
            public static void CollectDown(ref int i, ref int x)
            {
                if ((i + 1) <= (Field.frame.Count - 1) && Field.frame[i + 1][x] == gameField.diamond)
                {
                    Field.frame[i][x] = gameField.empty;
                    Field.frame[i + 1][x] = gameField.hero;
                    Console.SetCursorPosition(x, i);
                    Console.Write(gameField.empty);
                    Console.SetCursorPosition(x, i + 1);
                    Console.Write(gameField.hero);
                    AddScores();
                }
            }
            public static void CollectLeft(ref int i, ref int x)
            {
                if ((x - 1) >= 0 && Field.frame[i][x - 1] == gameField.diamond)
                {
                    Field.frame[i][x] = gameField.empty;
                    Field.frame[i][x - 1] = gameField.hero;
                    Console.SetCursorPosition(x, i);
                    Console.Write(gameField.empty);
                    Console.SetCursorPosition(x - 1, i);
                    Console.Write(gameField.hero);
                    AddScores();
                }
            }
            public static void CollectRight(ref int i, ref int x)
            {
                if ((x - 1) <= (Field.frame[i].Length - 1) && Field.frame[i][x + 1] == gameField.diamond)
                {
                    Field.frame[i][x] = gameField.empty;
                    Field.frame[i][x + 1] = gameField.hero;
                    Console.SetCursorPosition(x, i);
                    Console.Write(gameField.empty);
                    Console.SetCursorPosition(x + 1, i);
                    Console.Write(gameField.hero);
                    AddScores();
                }
            }
            public static int CountRock()
            {
                int c = 0;
                for (int i = Field.frame.Count - 1; i >= 0; i--)
                {
                    for (int x = Field.frame[i].Length - 1; x >= 0; x--)
                    {
                        if (Field.frame[i][x] == gameField.rock)
                        {
                            if (Field.frame[i + 1][x] == gameField.empty)
                            {
                                c++;
                            }
                        }
                    }
                }
                return c;
            }
            public static void MoveRock1()
            {
                for (int i = Field.frame.Count - 1; i >= 0; i--)
                {
                    for (int x = Field.frame[i].Length - 1; x >= 0; x--)
                    {
                        if (Field.frame[i][x] == gameField.rock)
                        {
                            if (Field.frame[i + 1][x] == gameField.hero)
                            {
                                lives = lives - 1;
                                Console.SetCursorPosition(1, 24);
                                Console.Write("Lives: " + gameField.lives);
                                if (lives == 0)
                                {
                                    gameField.Defeat();
                                    Field.frame[i][x] = gameField.empty;
                                    Field.frame[i + 1][x] = gameField.rock;
                                    Console.SetCursorPosition(x, i);
                                    Console.Write(gameField.empty);
                                    Console.SetCursorPosition(x, i + 1);
                                    Console.Write(gameField.rock);
                                }
                            }
                            else if (Field.frame[i + 1][x] == gameField.empty)
                            {
                                Field.frame[i][x] = gameField.empty;
                                Field.frame[i + 1][x] = gameField.rock;
                                Console.SetCursorPosition(x, i);
                                Console.Write(gameField.empty);
                                Console.SetCursorPosition(x, i + 1);
                                Console.Write(gameField.rock);
                                return;
                            }
                        }
                    }
                }
            }
            public static void BFS_step(int i1, int i2)
            {
                for (int i=0; i< BFS_x.Count; i++)
                {
                    if (BFS_x[i] == i1 && BFS_y[i] == i2)
                    {
                        return;
                    }
                }

                if (Field.frame[i1][i2] == gameField.diamond)
                {
                    gameField.BFS_score = gameField.BFS_score + 100;
                    BFS_x.Add(i1);
                    BFS_y.Add(i2);
                    BFS_step(i1 + 1, i2);
                    BFS_step(i1 - 1, i2);
                    BFS_step(i1, i2 + 1);
                    BFS_step(i1, i2 - 1);
                }
                else if (Field.frame[i1][i2] == gameField.hero || Field.frame[i1][i2] == gameField.sand || Field.frame[i1][i2] == gameField.empty)
                {
                    BFS_x.Add(i1);
                    BFS_y.Add(i2);
                    BFS_step(i1 + 1, i2);
                    BFS_step(i1 - 1, i2);
                    BFS_step(i1, i2 + 1);
                    BFS_step(i1, i2 - 1);
                }
                else
                {
                    BFS_x.Add(i1);
                    BFS_y.Add(i2);
                    return;
                }
            }

            public static bool BFS(int i1, int i2)
            {
                BFS_x.Clear();
                BFS_y.Clear();
                gameField.BFS_score = 0 + gameField.score;
                BFS_step(i1, i2);
                if (gameField.BFS_score >= gameField.maxpoint)
                {
                    return true;
                }
                return false;
            }

            public static int BFS_step_help(int i1, int i2, int distance)
            {
                distance++;
                int a = 1000, b = 1000, c = 1000, d = 1000;
                for (int i = 0; i < BFS_x_help.Count; i++)
                {
                    if (BFS_x_help[i] == i1 && BFS_y_help[i] == i2)
                    {
                        return 1000;
                    }
                }

                if (Field.frame[i1][i2] == gameField.diamond)
                {
                    BFS_x_help.Add(i1);
                    BFS_y_help.Add(i2);
                    return distance;
                }
                else if (Field.frame[i1][i2] == gameField.hero || Field.frame[i1][i2] == gameField.sand || Field.frame[i1][i2] == gameField.empty)
                {
                    BFS_x_help.Add(i1);
                    BFS_y_help.Add(i2);
                    a = BFS_step_help(i1 + 1, i2, distance);
                    b = BFS_step_help(i1 - 1, i2, distance);
                    c = BFS_step_help(i1, i2 + 1, distance);
                    d = BFS_step_help(i1, i2 - 1, distance);
                }
                else
                {
                    BFS_x_help.Add(i1);
                    BFS_y_help.Add(i2);
                    return 1000;
                }
                return Math.Min(Math.Min(a,b),Math.Min(c,d));
            }

            public static int BFS_help(int i1, int i2)
            {
                BFS_x_help.Clear();
                BFS_y_help.Clear();
                int distance = BFS_step_help(i1, i2, -1);
                return distance;
            }


            public static void Random()
            {
                do
                {
                    gameField.maxpoint = 0;
                    int temp;
                    for (int i = 1; i < Field.frame.Count - 1; i++)
                    {
                        for (int x = 1; x < Field.frame[i].Length - 1; x++)
                        {
                            temp = rnd.Next() % 100;
                            if (temp < 70)
                            {
                                Field.frame[i][x] = gameField.sand;
                            }
                            else if (temp < 80)
                            {
                                Field.frame[i][x] = gameField.diamond;
                                maxpoint += 100;
                            }
                            else if (temp < 100)
                            {
                                Field.frame[i][x] = gameField.rock;
                            }
                        }
                    }
                    Field.frame[1][1] = gameField.hero;
                    BFS_res = BFS(1, 1);
                }
                while (BFS_res == false);
                
            }
            public static void Random2()
            {
                int temp, bs = 0, bd = 0, br = 0;
                string prev = sand;
                do
                {
                    BFS_x.Clear();
                    BFS_y.Clear();

                    gameField.maxpoint = 0;
                    bs = 0; bd = 0; br=0;
                    prev = sand;
                    for (int i = 1; i < Field.frame.Count - 1; i++)
                    {
                        for (int x = 1; x < Field.frame[i].Length - 1; x++)
                        {
                            temp = rnd.Next() % 100;
                            if (prev == sand)
                            {
                                bs = 10;
                                bd = 0;
                                br = 0;
                            }
                            else if (prev == diamond)
                            {
                                bs = 0;
                                bd = 10;
                                br = 0;
                            }
                            else if (prev == rock)
                            {
                                bs = 0;
                                bd = 0;
                                br = 10;
                            }
                            if (temp < (70 + bs - bd - br))
                            {
                                Field.frame[i][x] = gameField.sand;
                                prev = sand;
                            }
                            else if (temp < 80 + bs + bd - br)
                            {
                                Field.frame[i][x] = gameField.diamond;
                                maxpoint += 100;
                                prev = diamond;
                            }
                            else if (temp < 100 + bs + bd + br)
                            {
                                Field.frame[i][x] = gameField.rock;
                                prev = rock;
                            }
                        }
                    }
                    Field.frame[1][1] = gameField.hero;
                    BFS_res = BFS(1, 1);
                }
                while (BFS_res == false);

            }
            public static void MoveRock2()
            {
                for (int i = 0; i <= Field.frame.Count - 1; i++)
                {
                    for (int x = 0; x <= Field.frame[i].Length - 1; x++)
                    {
                        if (Field.frame[i][x] == gameField.rock)
                        {
                            if (Field.frame[i + 1][x] == gameField.hero)
                            {
                                lives = lives - 1;
                                Console.SetCursorPosition(1, 24);
                                Console.Write("Lives: " + gameField.lives);
                                if (lives == 0)
                                {
                                    gameField.Defeat();
                                    Field.frame[i][x] = gameField.empty;
                                    Field.frame[i + 1][x] = gameField.rock;
                                    Console.SetCursorPosition(x, i);
                                    Console.Write(gameField.empty);
                                    Console.SetCursorPosition(x, i + 1);
                                    Console.Write(gameField.rock);
                                }
                            }
                            else if(Field.frame[i + 1][x] == gameField.empty)
                            {
                                Field.frame[i][x] = gameField.empty;
                                Field.frame[i + 1][x] = gameField.rock;
                                Console.SetCursorPosition(x, i);
                                Console.Write(gameField.empty);
                                Console.SetCursorPosition(x, i + 1);
                                Console.Write(gameField.rock);
                                return;
                            }
                        }
                    }
                }
            }

        }

        struct Fieldel
        {
            public string value;

            public Fieldel(string Value)
            {
                value = Value;
            }
        }


        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Thread music = new Thread(gameField.MusicFunction);
            music.Priority = ThreadPriority.Normal;
            music.Start();
            Thread thread = new Thread(gameField.ThreadFunction);
            
            thread.Priority = ThreadPriority.Normal;
            gameField.GetArrayFromFile("menu.txt");
            gameField.Renderer();
            thread.Start();
            while (true)
            {
                gameField.maxpoint = 400;
                int choose = -1;
                while (choose == -1)
                {
                    Console.SetCursorPosition(Field.frame[1].Length, Field.frame.Count);
                    var keyInfo = Console.ReadKey();
                    gameField.MoveHero(keyInfo);
                    if (gameField.score == 100)
                    {
                        for (int k = 6; k < Field.frame.Count; k++)
                        {
                            if (Field.frame[k][2] == gameField.hero)
                            {
                                choose = k - 5;
                                break;
                            }
                        }
                    }
                }

                Console.Clear();
                Field.frame.Clear();
                gameField.frame.Clear();
                Console.SetCursorPosition(0, 0);
                gameField.score = 0;
                switch (choose)
                {
                    case 1:
                        {
                            gameField.GetArrayFromFile("game.txt");
                            gameField.maxpoint = 3400;
                            break;
                        }
                    case 2:
                        {
                            gameField.GetArrayFromFile("2.txt");
                            gameField.maxpoint = 3400;
                            break;
                        }
                    case 3:
                        {
                            gameField.GetArrayFromFile("3.txt");
                            gameField.maxpoint = 3400;
                            break;
                        }
                    case 4:
                        {
                            gameField.GetArrayFromFile("4.txt");
                            gameField.Random2();
                            break;
                        }
                    case 5:
                        {
                            System.Environment.Exit(0);
                            break;
                        }
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                gameField.Renderer();
                Console.SetCursorPosition(12, 24);
                Console.Write("Score: " + gameField.score);
                Console.SetCursorPosition(1, 24);
                Console.Write("Lives: " + gameField.lives);
                
                
                while (gameField.gameStatus == true)
                {
                    Console.SetCursorPosition(Field.frame[1].Length, Field.frame.Count);
                    var keyInfo = Console.ReadKey();
                    gameField.MoveHero(keyInfo);
                    
                    if (gameField.score >= gameField.maxpoint)
                    {
                        break;
                    }
                    Console.SetCursorPosition(24, 24);
                    Console.Write("Deadlock: " + !gameField.BFS(gameField.y, gameField.x));
                    Console.Write(" ");
                    Console.SetCursorPosition(64, 24);
                    Console.Write("Steps to @: " + gameField.BFS_help(gameField.y, gameField.x));
                    Console.Write(" ");
                }

                gameField.score = 0;

                Console.Clear();
                Field.frame.Clear();
                gameField.frame.Clear();
                Console.SetCursorPosition(0, 0);
                gameField.GetArrayFromFile("menu.txt");
                gameField.Renderer();
            }
        }
      
    }
}
