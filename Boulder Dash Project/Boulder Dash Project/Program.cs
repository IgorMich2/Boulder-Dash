using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Media;

namespace Boulder_Dash_Project
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Thread music = new Thread(Music.MusicFunction);
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
                
                
                while (true)
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
