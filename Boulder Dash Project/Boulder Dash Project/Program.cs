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
            bool openfile = false;
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
                Hero.FindHero();
                Hero.steps = 0;
                Hero.digs = 0;
                GameField.maxpoint = 400;
                choose = -1;

                
                while (choose == -1)
                {
                    Console.SetCursorPosition(40, 25);
                    Console.Write("Last pressed key: ");
                    var keyInfo = Console.ReadKey();
                    Hero.MoveHero(keyInfo);
                    Console.Write("                   ");
                    if (GameField.score == 100)
                    {
                        for (int k = 7; k < Field.frame2.Count; k++)
                        {
                            if (Field.frame2[k][2].Value == new Hero().Value)
                            {
                                choose = k - 6;
                                break;
                            }
                        }
                    }
                }

                Console.Clear();
                Field.frame2.Clear();
                Console.SetCursorPosition(0, 0);
                GameField.score = 0;
                GameField.maxpoint = 0;
                Hero.steps = 0;
                Rock.RocksDownGravity = 0;
                Hero.RocksMoveByHero = 0;
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
                            GameField.GetArrayFromFile("4.txt");
                            GenerationLevel.Intellectual();
                            break;
                        }
                    case 6:
                        {
                            GameField.GetArrayFromFile("6.txt");
                            break;
                        }
                    case 7:
                        {
                            GameField.GetArrayFromFile("menu.txt");
                            Process.Start(new ProcessStartInfo(@"6.txt") { UseShellExecute = true });
                            openfile = true;
                            break;
                        }
                    case 8:
                        {
                            GameField.GetArrayFromFile("menu.txt");
                            GameField.GetResults();
                            openfile = true;
                            break;
                        }
                    case 9:
                        {
                            GameField.GetArrayFromFile("menu.txt");
                            GameField.GetBestResults();
                            openfile = true;
                            break;
                        }
                    case 10:
                        {
                            GameField.GetArrayFromFile("save.txt");
                            break;
                        }
                    case 11:
                        {
                            GameField.GetArrayFromFile("instruction.txt");
                            GameField.TechnicalLevel = true;
                            break;
                        }
                    case 12:
                        {
                            System.Environment.Exit(0);
                            break;
                        }
                }
                if (openfile == false && GameField.TechnicalLevel == false && GameField.Failedload == false)
                {
                    GameField.Time = DateTime.Now;
                    GameField.Renderer();
                    Hero.FindHero();
                    Console.SetCursorPosition(1, 27);
                    Console.Write("Score: " + GameField.score);
                    Console.SetCursorPosition(1, 26);
                    Console.Write("Lives: " + Hero.lives);

                    while (true)
                    {
                        Console.SetCursorPosition(40, 25);
                        Console.Write("Last pressed key: ");
                        var keyInfo = Console.ReadKey();
                        Console.Write("                   ");
                        Hero.MoveHero(keyInfo);

                        if (GameField.score >= GameField.maxpoint)
                        {
                            break;
                        }

                        Console.SetCursorPosition(1, 29);
                        Console.Write("Deadlock: " + !GenerationLevel.DFS(Hero.y, Hero.x) + " ");

                        Console.SetCursorPosition(1, 28);
                        Console.Write("Steps to @: " + GenerationLevel.BFSRadar(Hero.y, Hero.x));
                        Console.Write("    ");

                        Console.SetCursorPosition(1, 30);
                        Console.Write("Time : " + DateTime.Now.Subtract(GameField.Time));

                        Console.SetCursorPosition(40, 27);
                        Console.Write("Digs: " + Hero.digs + " ");
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
                else if (GameField.TechnicalLevel == true)
                {
                    GameField.Renderer();
                    Hero.FindHero();

                    while (true)
                    {
                        Console.SetCursorPosition(40, 25);
                        Console.Write("Last pressed key: ");
                        var keyInfo = Console.ReadKey();
                        Hero.MoveHero(keyInfo);
                        Console.Write("                   ");

                        if (GameField.score >= GameField.maxpoint)
                        {
                            break;
                        }
                    }
                }
                GameField.score = 0;
                openfile = false;
                Console.Clear();
                Field.frame2.Clear();
                Console.SetCursorPosition(0, 0);
                GameField.GetArrayFromFile("menu.txt");
                GameField.Renderer();
                GameField.TechnicalLevel = false;
            }
        }
      
    }
}
