using System;
using System.Collections.Generic;
using System.Text;
using NAudio.Wave;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Boulder_Dash_Project
{
    class MovingHero:Hero
    {
        public static void MoveHero(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
            {
                MovingHero.GoHero(Hero.y - 1, Hero.x);
            }
            else if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
            {
                MovingHero.GoHero(Hero.y + 1, Hero.x);
            }
            else if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
            {
                MovingHero.GoHero(Hero.y, Hero.x - 1);
            }
            else if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
            {
                MovingHero.GoHero(Hero.y, Hero.x + 1);
            }
            else if (keyInfo.Key == ConsoleKey.E)
            {
                MovingHero.GoDig(Hero.y, Hero.x + 1);
            }
            else if (keyInfo.Key == ConsoleKey.Q)
            {
                MovingHero.GoDig(Hero.y, Hero.x - 1);
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
            else if (x >= 0 && y >= 0 && y < Field.frame.Count && x < Field.frame[0].Count && x + (x - Hero.x) > 0 && x + (x - Hero.x) < Field.frame[0].Count && Math.Abs(x - Hero.x) == 1 && Field.frame[y][x + (x - Hero.x)].Value == new Empty().Value && Field.frame[y][x].Value == new Rock().Value)
            {
                Field.frame[y][Hero.x] = new Empty();
                Field.frame[y][x] = new Hero();
                Field.frame[y][x + (x - Hero.x)] = new Rock();
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
