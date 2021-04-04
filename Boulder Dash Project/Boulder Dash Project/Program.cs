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
        struct Field
        {
            public string value;

            public Field(string Value)
            {
                value = Value;
            }
        }

        static List<string[]> frame = new List<string[]>();
        static bool gameStatus = true;
        static int score = 0;
        static string hero = "I";
        static string rock = "o";
        static string diamond = "@";
        static string sand = "*";
        static string empty = " ";
        static public SoundPlayer player = new SoundPlayer();
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
                frame.Add(strline);
            }
        }

        static void Win()
        {
            player.Stop();
            player.SoundLocation = "win.wav";
            player.Play();

            Console.Clear();
            Console.WriteLine("Win!");
            Thread.Sleep(3000);
            gameStatus = false;
            Console.ReadKey();
        }

        static void Loose()
        {
            player.Stop();
            Console.Clear();
            Console.WriteLine("Loose!");
            Thread.Sleep(3000);
            gameStatus = false;
            Console.ReadKey();
        }

        static void ThreadFunction()
        {
            while (gameStatus == true)
            {
                Console.SetCursorPosition(frame[1].Length, frame.Count);
                MoveRock();
                Thread.Sleep(200);
            }
        }

        static void MusicFunction()
        {
            player.SoundLocation = "music.wav";
            while (gameStatus == true)
            {
                player.Play();
                Thread.Sleep(175000);
            }
            player.Stop();
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
            Console.Write("Score: " + score);
            
            while (gameStatus == true)
            {
                Console.SetCursorPosition(frame[1].Length, frame.Count);
                var keyInfo = Console.ReadKey();
                MoveHero(keyInfo);
            }
        }
        static void Renderer()
        {
            Console.Clear();
            for (int i = 0; i < frame.Count; i++)
            {
                Console.WriteLine(string.Join("", frame[i]));
            }

        }

        static void MoveHero(ConsoleKeyInfo keyInfo)
        {
            bool stat = true;
            for (int i = frame.Count - 1; i >= 0; i--)
            {
                for (int x = 0; x < frame[i].Length; x++)
                {
                    if (frame[i][x] == hero)
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
            score += 100;
            if (score >= 3400) Win();
            Console.SetCursorPosition(24, 24);
            Console.Write("Score: " + score);
        }
        static void GoUp(ref int i, ref int x, ref bool stat)
        {
            if ((i - 1) >= 0 && frame[i - 1][x] == sand || frame[i - 1][x] == empty)
            {
                frame[i][x] = empty;
                frame[i - 1][x] = hero;
                Console.SetCursorPosition(x, i);
                Console.Write(empty);
                Console.SetCursorPosition(x, i - 1);
                Console.Write(hero);
                stat = false;

            }
        }
        static void GoDown(ref int i, ref int x, ref bool stat)
        {
            if ((i + 1) <= (frame.Count - 1) && frame[i + 1][x] == sand || frame[i + 1][x] == empty)
            {
                frame[i][x] = empty;
                frame[i + 1][x] = hero;
                Console.SetCursorPosition(x, i);
                Console.Write(empty);
                Console.SetCursorPosition(x, i + 1);
                Console.Write(hero);
                stat = false;

            }
        }
        static void GoLeft(ref int i, ref int x, ref bool stat)
        {
            
            if((x - 1) >= 0 && frame[i][x - 1] == sand || frame[i][x - 1] == empty)
            {
                frame[i][x] = empty;
                frame[i][x - 1] = hero;
                Console.SetCursorPosition(x, i);
                Console.Write(empty);
                Console.SetCursorPosition(x - 1, i);
                Console.Write(hero);
                stat = false;

            }
            else if((x - 2) >= 0 && frame[i][x - 2] == empty && frame[i][x - 1] == rock)
            {
                frame[i][x] = empty;
                frame[i][x - 1] = hero;
                frame[i][x - 2] = rock;
                Console.SetCursorPosition(x, i);
                Console.Write(empty);
                Console.SetCursorPosition(x, i - 1);
                Console.Write(empty);
                Console.SetCursorPosition(x - 1, i);
                Console.Write(hero);
                Console.SetCursorPosition(x - 2, i);
                Console.Write(rock);
                stat = false;

            }
        }
        static void GoRight(ref int i, ref int x, ref bool stat)
        {
            if ((x - 1) <= (frame[i].Length - 1) && frame[i][x + 1] == sand || frame[i][x + 1] == empty)
            {

                frame[i][x] = empty;
                frame[i][x + 1] = hero;
                Console.SetCursorPosition(x, i);
                Console.Write(empty);
                Console.SetCursorPosition(x + 1, i);
                Console.Write(hero);
                stat = false;

            }
            else if ((x - 2) <= (frame[i].Length - 1) && frame[i][x + 1] == rock && frame[i][x + 2] == empty)
            {
                frame[i][x] = empty;
                frame[i][x + 1] = hero;
                frame[i][x + 2] = rock;
                Console.SetCursorPosition(x, i);
                Console.Write(empty);
                Console.SetCursorPosition(x + 1, i);
                Console.Write(hero);
                Console.SetCursorPosition(x + 2, i);
                Console.Write(rock);
                stat = false;
            }
        }
        static void CollectUp(ref int i, ref int x)
        {
            if ((i - 1) >= 0 && frame[i - 1][x] == diamond)
            {
                frame[i][x] = empty;
                frame[i - 1][x] = hero;
                Console.SetCursorPosition(x, i);
                Console.Write(empty);
                Console.SetCursorPosition(x, i - 1);
                Console.Write(hero);
                AddScores();
            }
        }
        static void CollectDown(ref int i, ref int x)
        {
            if ((i + 1) <= (frame.Count - 1) && frame[i + 1][x] == diamond)
            {
                frame[i][x] = empty;
                frame[i + 1][x] = hero;
                Console.SetCursorPosition(x, i);
                Console.Write(empty);
                Console.SetCursorPosition(x, i + 1);
                Console.Write(hero);
                AddScores();
            }
        }
        static void CollectLeft(ref int i, ref int x)
        {
            if ((x - 1) >= 0 && frame[i][x - 1] == diamond)
            {
                frame[i][x] = empty;
                frame[i][x - 1] = hero;
                Console.SetCursorPosition(x, i);
                Console.Write(empty);
                Console.SetCursorPosition(x - 1, i);
                Console.Write(hero);
                AddScores();
            }
        }
        static void CollectRight(ref int i, ref int x)
        {
            if ((x - 1) <= (frame[i].Length - 1) && frame[i][x + 1] == diamond)
            {
                frame[i][x] = empty;
                frame[i][x + 1] = hero;
                Console.SetCursorPosition(x, i);
                Console.Write(empty);
                Console.SetCursorPosition(x + 1, i);
                Console.Write(hero);
                AddScores();
            }
        }
        static void MoveRock()
        {
            for (int i = 0; i <= frame.Count - 1; i++)
            {
                for (int x = 0; x < frame[i].Length; x++)
                {
                    if (frame[i][x] == rock)
                    {
                        if (frame[i + 1][x] == hero)
                        {
                            frame[i][x] = empty;
                            frame[i + 1][x] = rock;
                            Console.SetCursorPosition(x, i);
                            Console.Write(empty);
                            Console.SetCursorPosition(x, i + 1);
                            Console.Write(rock);
                            Loose();
                        }
                        if (frame[i + 1][x] == empty)
                        {
                            frame[i][x] = empty;
                            frame[i + 1][x] = rock;
                            Console.SetCursorPosition(x, i);
                            Console.Write(empty);
                            Console.SetCursorPosition(x, i + 1);
                            Console.Write(rock);
                            return;
                        }
                    }
                }
            }
        }
    }
}
