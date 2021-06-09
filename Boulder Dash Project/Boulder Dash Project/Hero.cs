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

        public override char Value { get => 'I'; }
        public override bool CanEnter()
        {
            return true;
        }

        public static void FindHero()
        {
            for (int y = 0; y < Field.frame.Count; y++)
            {
                for (int x = 0; x < Field.frame[y].Count; x++)
                {
                    if (Field.frame[y][x].Value == new Hero().Value)
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
                Levels.SaveToFile();
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

            Output.Coordinates();
            Output.Steps();

        }

        public static void GoHero(int y, int x)
        {
            if (x >= 0 && y >= 0 && y < Field.frame.Count && x < Field.frame[0].Count && (Field.frame[y][x].CanEnter() == true))
            {
                bool isdiamond = false;
                if (Field.frame[y][x].Value == new Diamond().Value)
                {
                    isdiamond = true;
                }
                Field.frame[Hero.y][Hero.x] = new Empty();
                Field.frame[y][x] = new Hero();
                Output.PrintCell(Hero.x, Hero.y, new Empty());
                Output.PrintCell(x, y, new Hero()); 
                Hero.x = x;
                Hero.y = y;
                if (isdiamond)
                {
                    GameField.AddScores();
                }
            }
            else if (x >= 0 && y >= 0 && y < Field.frame.Count && x < Field.frame[0].Count && x + (x - Hero.x) > 0 && x + (x - Hero.x) < Field.frame[0].Count && Math.Abs(x - Hero.x) == 1 && Field.frame[y][x + (x-Hero.x)].Value == new Empty().Value && Field.frame[y][x].Value == new Rock().Value)
            {
                Field.frame[y][Hero.x] = new Empty();
                Field.frame[y][x] = new Hero();
                Field.frame[y][x + (x-Hero.x)] = new Rock();
                Output.PrintCell(Hero.x, Hero.y, new Empty());
                Output.PrintCell(x, y, new Hero());
                Output.PrintCell(x + (x - Hero.x), y, new Rock());
                Hero.x = x;
                Hero.y = y;
                RocksMoveByHero++;
            }
            steps++;
        }
        public static void GoDig(int y, int x)
        {
            if (Field.frame[y][x].Value == new Sand().Value)
            {
                Field.frame[y][x] = new Empty();
                Output.PrintCell(x, y, new Empty());
                digs++;
            }
        }
    }
}
