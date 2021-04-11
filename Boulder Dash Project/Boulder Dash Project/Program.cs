using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Media;
using System.Windows;


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
            static public SoundPlayer player = new SoundPlayer();

        }

        class gameField : Field
        {
            public static List<string[]> frame = new List<string[]>();
            public static bool gameStatus = true;

            public static int score = 0;
            public static string hero = "I";
            public static string rock = "o";
            public static string diamond = "@";
            public static string sand = "*";
            public static string empty = " ";
            static public SoundPlayer player = new SoundPlayer();
        }

        struct Fieldel
        {
            public string value;

            public Fieldel(string Value)
            {
                value = Value;
            }
        }

        
        
        static void GetArrayFromFile(string fileName)
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

        static void Win()
        {
            Field.player.Stop();
            Field.player.SoundLocation = "win.wav";
            Field.player.Play();

            Console.Clear();
            Console.WriteLine("Win!");
            Thread.Sleep(3000);
            gameField.gameStatus = false;
            System.Environment.Exit(0);
        }

        static void Loose()
        {
            Field.player.Stop();
            Console.Clear();
            Console.WriteLine("Loose!");
            Thread.Sleep(3000);
            gameField.gameStatus = false;
            System.Environment.Exit(0);
        }

        static void ThreadFunction()
        {
            while (gameField.gameStatus == true)
            {
                Console.SetCursorPosition(Field.frame[1].Length, Field.frame.Count);
                MoveRock();
                Thread.Sleep(200);
            }
        }

        static void MusicFunction()
        {
            Field.player.SoundLocation = "music.wav";
            while (gameField.gameStatus == true)
            {
                Field.player.Play();
                Thread.Sleep(175000);
            }
            Field.player.Stop();
        }

        static void Main(string[] args)
        {
            GetArrayFromFile("game.txt");
            Thread thread = new Thread(ThreadFunction);
            Thread music = new Thread(MusicFunction);
            thread.Start();
            music.Start();
            thread.Priority = ThreadPriority.Normal;
            music.Priority = ThreadPriority.Normal;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Renderer();
            Console.SetCursorPosition(24, 24);
            Console.Write("Score: " + gameField.score);
            
            while (gameField.gameStatus == true)
            {
                Console.SetCursorPosition(Field.frame[1].Length, Field.frame.Count);
                var keyInfo = Console.ReadKey();
                MoveHero(keyInfo);
            }
        }
        static void Renderer()
        {
            Console.Clear();
            for (int i = 0; i < Field.frame.Count; i++)
            {
                Console.WriteLine(string.Join("", Field.frame[i]));
            }

        }

        static void MoveHero(ConsoleKeyInfo keyInfo)
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
                        break;
                    }

                }
            }
        }
        static void AddScores()
        {
            gameField.score += 100;
            if (gameField.score >= 3400) Win();
            Console.SetCursorPosition(24, 24);
            Console.Write("Score: " + gameField.score);
        }
        static void GoUp(ref int i, ref int x, ref bool stat)
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
        static void GoDown(ref int i, ref int x, ref bool stat)
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
        static void GoLeft(ref int i, ref int x, ref bool stat)
        {
            
            if((x - 1) >= 0 && Field.frame[i][x - 1] == gameField.sand || Field.frame[i][x - 1] == gameField.empty)
            {
                Field.frame[i][x] = gameField.empty;
                Field.frame[i][x - 1] = gameField.hero;
                Console.SetCursorPosition(x, i);
                Console.Write(gameField.empty);
                Console.SetCursorPosition(x - 1, i);
                Console.Write(gameField.hero);
                stat = false;

            }
            else if((x - 2) >= 0 && Field.frame[i][x - 2] == gameField.empty && Field.frame[i][x - 1] == gameField.rock)
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
        static void GoRight(ref int i, ref int x, ref bool stat)
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
        static void CollectUp(ref int i, ref int x)
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
        static void CollectDown(ref int i, ref int x)
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
        static void CollectLeft(ref int i, ref int x)
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
        static void CollectRight(ref int i, ref int x)
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
        static void MoveRock()
        {
            for (int i = 0; i <= Field.frame.Count - 1; i++)
            {
                for (int x = 0; x < Field.frame[i].Length; x++)
                {
                    if (Field.frame[i][x] == gameField.rock)
                    {
                        if (Field.frame[i + 1][x] == gameField.hero)
                        {
                            Field.frame[i][x] = gameField.empty;
                            Field.frame[i + 1][x] = gameField.rock;
                            Console.SetCursorPosition(x, i);
                            Console.Write(gameField.empty);
                            Console.SetCursorPosition(x, i + 1);
                            Console.Write(gameField.rock);
                            Loose();
                        }
                        if (Field.frame[i + 1][x] == gameField.empty)
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
}
