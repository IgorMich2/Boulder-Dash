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
            Logic.SetColor();

            Thread music = new Thread(Music.MusicFunction);
            music.Priority = ThreadPriority.Normal;
            
            Thread lives = new Thread(Lives.LivesFunction);
            lives.Priority = ThreadPriority.Highest;

            Thread gravity = new Thread(Gravity.GravityFunction);
            gravity.Priority = ThreadPriority.Normal;

            Levels.GetArrayFromFile("menu.txt");
            Levels.Renderer();

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
                    Logic.LastPressedKey();
                    var keyInfo = Logic.GetKey();
                    Hero.MoveHero(keyInfo);
                    Logic.BigSpace();
                    if (GameField.score == 100)
                    {
                        for (int k = 7; k < Field.frame.Count; k++)
                        {
                            if (Field.frame[k][2].Value == new Hero().Value)
                            {
                                choose = k - 6;
                                break;
                            }
                        }
                    }
                }

                Logic.Clear();
                Field.frame.Clear();
                Logic.SetToZero();
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
                            Levels.GetArrayFromFile("1.txt");    
                            break;
                        }
                    case 2:
                        {
                            Levels.GetArrayFromFile("2.txt");
                            break;
                        }
                    case 3:
                        {
                            Levels.GetArrayFromFile("3.txt");
                            break;
                        }
                    case 4:
                        {
                            Levels.GetArrayFromFile("4.txt");
                            GenerationLevel.Random2();
                            break;
                        }
                    case 5:
                        {
                            Levels.GetArrayFromFile("4.txt");
                            GenerationLevel.Intellectual();
                            break;
                        }
                    case 6:
                        {
                            Levels.GetArrayFromFile("6.txt");
                            break;
                        }
                    case 7:
                        {
                            Levels.GetArrayFromFile("menu.txt");
                            Process.Start(new ProcessStartInfo(@"6.txt") { UseShellExecute = true });
                            openfile = true;
                            break;
                        }
                    case 8:
                        {
                            Levels.GetArrayFromFile("menu.txt");
                            Database.GetResults();
                            openfile = true;
                            break;
                        }
                    case 9:
                        {
                            Levels.GetArrayFromFile("menu.txt");
                            Database.GetBestResults();
                            openfile = true;
                            break;
                        }
                    case 10:
                        {
                            Levels.GetArrayFromFile("save.txt");
                            break;
                        }
                    case 11:
                        {
                            Levels.GetArrayFromFile("instruction.txt");
                            GameField.TechnicalLevel = true;
                            break;
                        }
                    case 12:
                        {
                            System.Environment.Exit(0);
                            break;
                        }
                }
                if (openfile == false && GameField.TechnicalLevel == false && Levels.Failedload == false)
                {
                    GameField.Time = DateTime.Now;
                    Levels.Renderer();
                    Hero.FindHero();
                    Logic.Score();
                    Logic.Lives();
                    GameField.GameStatus = true;
                    while (true)
                    {
                        Logic.LastPressedKey();
                        var keyInfo = Logic.GetKey();
                        Logic.BigSpace();
                        Hero.MoveHero(keyInfo);

                        if (GameField.score >= GameField.maxpoint)
                        {
                            break;
                        }

                        Logic.Deadlock();
                        Logic.Radar();
                        Logic.Time();
                        Logic.Digs();
                    }
                    GameField.GameStatus = false;
                    if (GameField.win == true)
                    {
                        Database.EndLevel("Win");
                    }
                    else
                    {
                        Database.EndLevel("Defeat");
                    }
                }
                else if (GameField.TechnicalLevel == true)
                {
                    Levels.Renderer();
                    Hero.FindHero();

                    while (true)
                    {
                        Logic.LastPressedKey();
                        var keyInfo = Logic.GetKey();
                        Hero.MoveHero(keyInfo);
                        Logic.BigSpace();

                        if (GameField.score >= GameField.maxpoint)
                        {
                            break;
                        }
                    }
                }
                GameField.score = 0;
                openfile = false;
                Logic.Clear();
                Field.frame.Clear();
                Logic.SetToZero();
                Logic.SetToZero();
                Levels.GetArrayFromFile("menu.txt");
                Levels.Renderer();
                GameField.TechnicalLevel = false;
            }
        }
      
    }
}
