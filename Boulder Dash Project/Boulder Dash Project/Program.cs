using System;
using System.Threading;

namespace Boulder_Dash_Project
{
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

                switch (choose)
                {
                    case 1:
                        {
                            GameField.GetArrayFromFile("1.txt");
                            GameField.maxpoint = 3400;
                            break;
                        }
                    case 2:
                        {
                            GameField.GetArrayFromFile("2.txt");
                            GameField.maxpoint = 3400;
                            break;
                        }
                    case 3:
                        {
                            GameField.GetArrayFromFile("3.txt");
                            GameField.maxpoint = 3400;
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
                            System.Environment.Exit(0);
                            break;
                        }
                }

                //Console.ForegroundColor = ConsoleColor.Cyan;
                GameField.Renderer();
                Console.SetCursorPosition(12, 24);
                Console.Write("Score: " + GameField.score);
                Console.SetCursorPosition(1, 24);
                Console.Write("Lives: " + GameField.lives);
                
                while (true)
                {
                    Console.SetCursorPosition(Field.frame[1].Length, Field.frame.Count);
                    var keyInfo = Console.ReadKey();
                    Hero.MoveHero(keyInfo);
                    
                    if (GameField.score >= GameField.maxpoint)
                    {
                        break;
                    }
                    Console.SetCursorPosition(24, 24);
                    Console.Write("Deadlock: " + !GenerationLevel.BFS(GameField.y, GameField.x) + " ");

                    Console.SetCursorPosition(64, 24);
                    Console.Write("Steps to @: " + GenerationLevel.BFS_help(GameField.y, GameField.x) + " ");
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
