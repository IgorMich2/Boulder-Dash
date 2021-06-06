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


        /*public override string Value
        {
            get { return value; }
        }*/

        public static void FindHero()
        {
            for (int y = 0; y < Field.frame.Count; y++)
            {
                for (int x = 0; x < Field.frame[y].Length; x++)
                {
                    if (Field.frame[y][x] == Hero.value)
                    {
                        Hero.x = x;
                        Hero.y = y;
                        y = 10000;
                        break;
                    }
                }
            }
        }

        public static void MoveHero(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
            {
                Hero.GoHero(Hero.y - 1, Hero.x);
            }
            else if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
            {
                Hero.GoHero(Hero.y + 1, Hero.x);
            }
            else if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
            {
                Hero.GoHero(Hero.y, Hero.x-1);
            }
            else if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
            {
                Hero.GoHero(Hero.y, Hero.x + 1);
            }
            else if (keyInfo.Key == ConsoleKey.E)
            {
                Hero.GoDig(Hero.y, Hero.x+1);
            }
            else if (keyInfo.Key == ConsoleKey.Q)
            {
                Hero.GoDig(Hero.y, Hero.x-1);
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
            else if (keyInfo.Key == ConsoleKey.T)
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

            Console.SetCursorPosition(40, 28);
            Console.Write("Coordinates: x=" + Hero.x + ", y=" + Hero.y + " ");
            Console.SetCursorPosition(40, 26);
            Console.Write("Steps: " + steps + " ");

        }

        public static void GoHero(int y, int x)
        {
            if (x >= 0 && y >= 0 && (Field.frame[y][x] == Sand.value || Field.frame[y][x] == Empty.value))
            {
                Field.frame[Hero.y][Hero.x] = Empty.value;
                Field.frame[y][x] = Hero.value;
                Console.SetCursorPosition(Hero.x, Hero.y);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x, y);
                Console.Write(Hero.value);
                Hero.x = x;
                Hero.y = y;
            }
            else if (Field.frame[y][x] == Diamond.value)
            {
                Field.frame[Hero.y][Hero.x] = Empty.value;
                Field.frame[y][x] = Hero.value;
                Console.SetCursorPosition(Hero.x, Hero.y);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x, y);
                Console.Write(Hero.value);
                GameField.AddScores();
                Hero.x = x;
                Hero.y = y;
            }
            else if (x + (x - Hero.x) > 0 && x + (x - Hero.x) < Field.frame[0].Length && Math.Abs(x - Hero.x) == 1 && Field.frame[y][x + (x-Hero.x)] == Empty.value && Field.frame[y][x] == Rock.value)
            {
                Field.frame[y][Hero.x] = Empty.value;
                Field.frame[y][x] = Hero.value;
                Field.frame[y][x + (x-Hero.x)] = Rock.value;
                Console.SetCursorPosition(Hero.x, Hero.y);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x, y);
                Console.Write(Hero.value);
                Console.SetCursorPosition(x + (x-Hero.x), y);
                Console.Write(Rock.value);
                Hero.x = x;
                Hero.y = y;
                RocksMoveByHero++;
            }
            steps++;
        }
        public static void GoDig(int y, int x)
        {
            if (Field.frame[y][x] == Sand.value)
            {
                Field.frame[y][x] = Empty.value;
                Console.SetCursorPosition(x, y);
                Console.Write(Empty.value);
                digs++;
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
