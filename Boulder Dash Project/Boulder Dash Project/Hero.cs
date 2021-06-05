using System;
using NAudio.Wave;
using System.Threading;

namespace Boulder_Dash_Project
{
    class Hero : Cell
    {
        public static int lives = 500;
        public static int x;
        public static int y;
        public static int steps;
        public static int digs;
        public static string value = "I";
        public static int RocksMoveByHero = 0;


        public override string Value
        {
            get { return value; }
        }

        public static void MoveHero(ConsoleKeyInfo keyInfo)
        {
            for (int y = Field.frame.Count - 1; y >= 0; y--)
            {
                for (int x = 0; x < Field.frame[y].Length; x++)
                {
                    if (Field.frame[y][x] == Hero.value)
                    {
                        if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
                        {
                            Hero.GoUp(ref y, ref x);
                            steps++;
                        }
                        else if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
                        {
                            Hero.GoDown(ref y, ref x);
                            steps++;
                        }
                        else if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
                        {
                            Hero.GoLeft(ref y, ref x);
                            steps++;
                        }
                        else if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
                        {
                            Hero.GoRight(ref y, ref x);
                            steps++;
                        }
                        else if (keyInfo.Key == ConsoleKey.E)
                        {
                            Hero.GoDigRight(ref y, ref x);
                            digs++;
                        }
                        else if (keyInfo.Key == ConsoleKey.Q)
                        {
                            Hero.GoDigLeft(ref y, ref x);
                            digs++;
                        }
                        else if (keyInfo.Key == ConsoleKey.L)
                        {
                            GameField.score = GameField.maxpoint;
                            GameField.Defeat();
                        }
                        else if (keyInfo.Key == ConsoleKey.K)
                        {
                            GameField.Win();
                        }
                        else if (keyInfo.Key == ConsoleKey.F)
                        {
                            GameField.SaveToFile();
                        }
                        else if(keyInfo.Key == ConsoleKey.T)
                        {
                            Music.waveOut.Volume = Convert.ToSingle(Math.Min(Music.waveOut.Volume + Convert.ToSingle(0.1), 1.0));
                        }
                        else if (keyInfo.Key == ConsoleKey.G)
                        {
                            Music.waveOut.Volume = Convert.ToSingle(Math.Max(Music.waveOut.Volume - Convert.ToSingle(0.1), 0));
                        }
                        else if (keyInfo.Key == ConsoleKey.N)
                        {
                            Music.waveOut.Stop();
                            Thread music = new Thread(Music.MusicFunction);
                            music.Priority = ThreadPriority.Normal;
                            music.Start();
                        }
                        y = -1;
                        break;
                    }
                }
            }
            Console.SetCursorPosition(40, 28);
            Console.Write("Coordinates: x=" + Hero.x + ", y=" + Hero.y + " ");
            Console.SetCursorPosition(40, 26);
            Console.Write("Steps: " + steps + " ");
            Console.SetCursorPosition(40, 27);
            Console.Write("Digs: " + digs + " ");
        }

        public static void GoUp(ref int y, ref int x)
        {
            if ((y - 1) >= 0 && (Field.frame[y - 1][x] == Sand.value || Field.frame[y - 1][x] == Empty.value))
            {
                Field.frame[y][x] = Empty.value;
                Field.frame[y - 1][x] = Hero.value;
                Console.SetCursorPosition(x, y);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x, y - 1);
                Console.Write(Hero.value);
                Hero.x = x;
                Hero.y = y - 1;
            }
            else if ((y - 1) >= 0 && Field.frame[y - 1][x] == Diamond.value)
            {
                Field.frame[y][x] = Empty.value;
                Field.frame[y - 1][x] = Hero.value;
                Console.SetCursorPosition(x, y);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x, y - 1);
                Console.Write(Hero.value);
                GameField.AddScores();
            }


        }
        public static void GoDown(ref int y, ref int x)
        {
            if ((y + 1) <= (Field.frame.Count - 1) && (Field.frame[y + 1][x] == Sand.value || Field.frame[y + 1][x] == Empty.value))
            {
                Field.frame[y][x] = Empty.value;
                Field.frame[y + 1][x] = Hero.value;
                Console.SetCursorPosition(x, y);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x, y + 1);
                Console.Write(Hero.value);
                Hero.x = x;
                Hero.y = y + 1;

            }
            else if ((y + 1) <= (Field.frame.Count - 1) && Field.frame[y + 1][x] == Diamond.value)
            {
                Field.frame[y][x] = Empty.value;
                Field.frame[y + 1][x] = Hero.value;
                Console.SetCursorPosition(x, y);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x, y + 1);
                Console.Write(Hero.value);
                GameField.AddScores();
            }

        }
        public static void GoLeft(ref int y, ref int x)
        {
            if ((x - 1) >= 0 && (Field.frame[y][x - 1] == Sand.value || Field.frame[y][x - 1] == Empty.value))
            {
                Field.frame[y][x] = Empty.value;
                Field.frame[y][x - 1] = Hero.value;
                Console.SetCursorPosition(x, y);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x - 1, y);
                Console.Write(Hero.value);
                Hero.x = x - 1;
                Hero.y = y;
            }
            else if ((x - 2) >= 0 && Field.frame[y][x - 2] == Empty.value && Field.frame[y][x - 1] == Rock.value)
            {
                Field.frame[y][x] = Empty.value;
                Field.frame[y][x - 1] = Hero.value;
                Field.frame[y][x - 2] = Rock.value;
                Console.SetCursorPosition(x, y);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x, y - 1);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x - 1, y);
                Console.Write(Hero.value);
                Console.SetCursorPosition(x - 2, y);
                Console.Write(Rock.value);
                Hero.x = x - 1;
                Hero.y = y;
                RocksMoveByHero++;
            }
            else if ((x - 1) >= 0 && Field.frame[y][x - 1] == Diamond.value)
            {
                Field.frame[y][x] = Empty.value;
                Field.frame[y][x - 1] = Hero.value;
                Console.SetCursorPosition(x, y);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x - 1, y);
                Console.Write(Hero.value);
                GameField.AddScores();
            }

        }
        public static void GoRight(ref int y, ref int x)
        {
            if ((x - 1) <= (Field.frame[y].Length - 1) && (Field.frame[y][x + 1] == Sand.value || Field.frame[y][x + 1] == Empty.value))
            {
                Field.frame[y][x] = Empty.value;
                Field.frame[y][x + 1] = Hero.value;
                Console.SetCursorPosition(x, y);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x + 1, y);
                Console.Write(Hero.value);
                Hero.x = x + 1;
                Hero.y = y;
            }
            else if ((x - 2) <= (Field.frame[y].Length - 1) && Field.frame[y][x + 1] == Rock.value && Field.frame[y][x + 2] == Empty.value)
            {
                Field.frame[y][x] = Empty.value;
                Field.frame[y][x + 1] = Hero.value;
                Field.frame[y][x + 2] = Rock.value;
                Console.SetCursorPosition(x, y);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x + 1, y);
                Console.Write(Hero.value);
                Console.SetCursorPosition(x + 2, y);
                Console.Write(Rock.value);
                Hero.x = x + 1;
                Hero.y = y;
                RocksMoveByHero++;
            }
            else if ((x - 1) <= (Field.frame[y].Length - 1) && Field.frame[y][x + 1] == Diamond.value)
            {
                Field.frame[y][x] = Empty.value;
                Field.frame[y][x + 1] = Hero.value;
                Console.SetCursorPosition(x, y);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x + 1, y);
                Console.Write(Hero.value);
                GameField.AddScores();
            }
        }
        public static void GoDigRight(ref int y, ref int x)
        {
            if ((x - 1) <= (Field.frame[y].Length - 1) && Field.frame[y][x + 1] == Sand.value)
            {
                Field.frame[y][x + 1] = Empty.value;
                Console.SetCursorPosition(x + 1, y);
                Console.Write(Empty.value);
            }
        }
        public static void GoDigLeft(ref int y, ref int x)
        {
            if ((x - 1) >= 0 && Field.frame[y][x - 1] == Sand.value)
            {
                Field.frame[y][x - 1] = Empty.value;
                Console.SetCursorPosition(x - 1, y);
                Console.Write(Empty.value);
            }
        }
    }
}
