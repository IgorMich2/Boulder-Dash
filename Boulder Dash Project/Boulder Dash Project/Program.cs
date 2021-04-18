using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Media;



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

            public static void MusicFunction()
            {
                int i = rnd.Next() % 4;
                if (i == 0)
                {
                    Field.player.SoundLocation = "music.wav";
                    while (gameField.gameStatus == true)
                    {
                        Field.player.Play();
                        Thread.Sleep(175000);
                    }
                    Field.player.Stop();
                }
                else if (i==1)
                {
                    Field.player.SoundLocation = "music2.wav";
                    while (gameField.gameStatus == true)
                    {
                        Field.player.Play();
                        Thread.Sleep(101000);
                    }
                    Field.player.Stop();
                }
                else if (i == 2)
                {
                    Field.player.SoundLocation = "music3.wav";
                    while (gameField.gameStatus == true)
                    {
                        Field.player.Play();
                        Thread.Sleep(308000);
                    }
                    Field.player.Stop();
                }
                else if (i == 3)
                {
                    Field.player.SoundLocation = "music4.wav";
                    while (gameField.gameStatus == true)
                    {
                        Field.player.Play();
                        Thread.Sleep(193000);
                    }
                    Field.player.Stop();
                }
            }

            public static void Win()
            {
                Field.player.Stop();
                Field.player.SoundLocation = "win.wav";
                Field.player.Play();

                Console.Clear();
                Console.WriteLine("Win!");
                Thread.Sleep(5000);


                gameField.gameStatus = false;
                System.Environment.Exit(0);

            }

            public static void Loose()
            {
                Field.player.Stop();
                Console.Clear();
                Console.WriteLine("Loose!");
                Thread.Sleep(3000);

                gameField.gameStatus = false;
                System.Environment.Exit(0);
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
                    else
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
                Console.WriteLine(BFS_res);
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
                                if (stat) GoUp(ref i, ref x, ref stat);
                                CollectUp(ref i, ref x);
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
                            if (keyInfo.Key == ConsoleKey.F)
                            {
                                if (stat) GoDig(ref i, ref x, ref stat);
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
                Console.SetCursorPosition(24, 24);
                Console.Write("Score: " + gameField.score);
            }
            public static void GoUp(ref int i, ref int x, ref bool stat)
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

                }
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
                                Console.SetCursorPosition(10, 24);
                                Console.Write("Lives: " + gameField.lives);
                                if (lives == 0)
                                {
                                    gameField.Loose();
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
                else if (Field.frame[i1][i2] == gameField.hero || Field.frame[i1][i2] == gameField.sand)
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
                gameField.BFS_score = 0;
                BFS_step(i1, i2);
                if (gameField.BFS_score == gameField.maxpoint)
                {
                    return true;
                }
                return false;
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
                                Console.SetCursorPosition(10, 24);
                                Console.Write("Lives: " + gameField.lives);
                                if (lives == 0)
                                {
                                    gameField.Loose();
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
            Thread music = new Thread(gameField.MusicFunction);
            music.Priority = ThreadPriority.Normal;
            music.Start();
            Thread thread = new Thread(gameField.ThreadFunction);
            
            thread.Priority = ThreadPriority.Normal;

                int x = 0;
                Console.WriteLine("____________________________________");
                Console.WriteLine("|Boulder Dash Game by Ihor Michurin|");
                Console.WriteLine("____________________________________");
                Console.WriteLine("|             Main Menu            |");
                Console.WriteLine("____________________________________");
                Console.WriteLine("");
                Console.WriteLine("Choose level: 1, 2, 3 or enter 4 to generate random level.");
                int i = Convert.ToInt32(Console.ReadLine());
                if (i == 1)
                {
                    gameField.GetArrayFromFile("game.txt");
                    gameField.maxpoint = 3400;
                }
                else if (i == 2)
                {
                    gameField.GetArrayFromFile("2.txt");
                    gameField.maxpoint = 3400;

                }
                else if (i == 3)
                {
                    gameField.GetArrayFromFile("3.txt");
                    gameField.maxpoint = 3400;

                }
                else if (i == 4)
                {
                    gameField.GetArrayFromFile("4.txt");
                    gameField.Random();
                }


                
                thread.Start();


                Console.ForegroundColor = ConsoleColor.Cyan;
                gameField.Renderer();
                Console.SetCursorPosition(24, 24);
                Console.Write("Score: " + gameField.score);
                Console.SetCursorPosition(10, 24);
                Console.Write("Lives: " + gameField.lives);
                while (gameField.gameStatus == true)
                {
                    Console.SetCursorPosition(Field.frame[1].Length, Field.frame.Count);
                    var keyInfo = Console.ReadKey();
                    gameField.MoveHero(keyInfo);
                }

                
            
        }
      
    }
}
