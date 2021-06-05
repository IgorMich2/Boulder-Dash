using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Boulder_Dash_Project
{//
    class Program
    {
        static void Main(string[] args)
        {
            int choose;

            Console.ForegroundColor = ConsoleColor.Cyan;

            Thread music = new Thread(Music.MusicFunction);
            music.Priority = ThreadPriority.Normal;
            
            Thread lives = new Thread(GameField.LivesFunction);
            lives.Priority = ThreadPriority.Highest;

            Thread gravity = new Thread(GameField.GravityFunction);
            gravity.Priority = ThreadPriority.Normal;

            GameField.GetArrayFromFile("menu.txt");
            GameField.Renderer();

            music.Start();
            lives.Start();
            gravity.Start();



            while (true)
            {
                Hero.steps = 0;
                Hero.digs = 0;
                GameField.maxpoint = 400;
                choose = -1;

                
                while (choose == -1)
                {
                    Console.SetCursorPosition(Field.frame[1].Length, Field.frame.Count);
                    var keyInfo = Console.ReadKey();
                    Hero.MoveHero(keyInfo);
                    if (GameField.score == 100)
                    {
                        for (int k = 6; k < Field.frame.Count; k++)
                        {
                            if (Field.frame[k][2] == Hero.value)
                            {
                                choose = k - 5;
                                break;
                            }
                        }
                    }
                }

                Console.Clear();
                Field.frame.Clear();
                Console.SetCursorPosition(0, 0);
                GameField.score = 0;
                GameField.maxpoint = 0;
                Hero.steps = 0;
                Hero.digs = 0;
                switch (choose)
                {
                    case 1:
                        {
                            GameField.GetArrayFromFile("1.txt");    
                            break;
                        }
                    case 2:
                        {
                            GameField.GetArrayFromFile("2.txt");
                            break;
                        }
                    case 3:
                        {
                            GameField.GetArrayFromFile("3.txt");
                            break;
                        }
                    case 4:
                        {
                            GameField.GetArrayFromFile("4.txt");
                            GenerationLevel.Random2();
                            break;
                        }
                    case 5:
                        {
                            GameField.GetArrayFromFile(@"6.txt");
                            break;
                        }
                    case 6:
                        {
                            GameField.GetArrayFromFile("menu.txt");
                            Process.Start(new ProcessStartInfo(@"6.txt") { UseShellExecute = true });
                            break;
                        }
                    case 7:
                        {
                            GameField.GetArrayFromFile("menu.txt");
                            GameField.GetResults();
                            break;
                        }
                    case 8:
                        {
                            GameField.GetArrayFromFile("menu.txt");
                            GameField.GetBestResults();
                            break;
                        }
                    case 9:
                        {
                            GameField.GetArrayFromFile("save.txt");
                            break;
                        }
                    case 10:
                        {
                            GameField.GetArrayFromFile("menu.txt");
                            Process.Start(new ProcessStartInfo(@"instruction.txt") { UseShellExecute = true });
                            break;
                        }
                    case 11:
                        {
                            System.Environment.Exit(0);
                            break;
                        }
                }
                if (choose != 7 && choose != 6 && choose != 8 && choose != 10)
                {
                    GameField.Time = DateTime.Now;
                    //Console.ForegroundColor = ConsoleColor.Cyan;
                    GameField.Renderer();
                    Console.SetCursorPosition(1, 27);
                    Console.Write("Score: " + GameField.score);
                    Console.SetCursorPosition(1, 26);
                    Console.Write("Lives: " + Hero.lives);

                    while (true)
                    {
                        Console.SetCursorPosition(Field.frame[1].Length, Field.frame.Count);
                        var keyInfo = Console.ReadKey();
                        Hero.MoveHero(keyInfo);

                        if (GameField.score >= GameField.maxpoint)
                        {
                            break;
                        }

                        Console.SetCursorPosition(1, 29);
                        Console.Write("Deadlock: " + !GenerationLevel.BFS(Hero.y, Hero.x) + " ");

                        Console.SetCursorPosition(1, 28);
                        Console.Write("Steps to @: " + GenerationLevel.BFS_help(Hero.y, Hero.x));
                        Console.Write("    ");

                        Console.SetCursorPosition(1, 30);
                        Console.Write("Time : " + DateTime.Now.Subtract(GameField.Time));
                    }

                    if (GameField.win == true)
                    {
                        GameField.EndLevel("Win");
                    }
                    else
                    {
                        GameField.EndLevel("Defeat");
                    }
                }
                GameField.score = 0;

                Console.Clear();
                Field.frame.Clear();
                Console.SetCursorPosition(0, 0);
                GameField.GetArrayFromFile("menu.txt");
                GameField.Renderer();
            }
        }
      
    }
}
