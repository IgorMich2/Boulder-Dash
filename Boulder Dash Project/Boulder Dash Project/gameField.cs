using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace Boulder_Dash_Project
{
    class gameField : Field
    {
        public static Random rnd = new Random();
        public static int score = 0;
        public static int maxpoint = 0;
        public static int lives = 500;
        public static bool BFS_res = false;
        public static int BFS_score;

        public static List<int> BFS_x = new List<int>();
        public static List<int> BFS_y = new List<int>();
        public static List<int> BFS_x_help = new List<int>();
        public static List<int> BFS_y_help = new List<int>();
        public static bool Deadlock = false;
        public static int x;
        public static int y;


        public static void Win()
        {

            Field.player.SoundLocation = "win.wav";
            Field.player.Play();

            Console.Clear();
            Console.WriteLine("Win!");
            Thread.Sleep(5000);
            Field.player.Stop();
            gameField.score = gameField.maxpoint;
            Console.Clear();

        }

        public static void Defeat()
        {
            Console.Clear();
            Field.player.Stop();
            Field.player.SoundLocation = "loose.wav";
            Field.player.Play();

            Console.WriteLine("Defeat!");
            Thread.Sleep(3000);
            Field.player.Stop();
            gameField.score = gameField.maxpoint;
        }

        public static void GetArrayFromFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            int rowCount = lines.Length;

            for (int i = 0; i < rowCount - 1; i++)
            {
                char[] line = lines[i + 1].ToCharArray();
                string[] strline = new string[line.Length];
                for (int k = 0; k < line.Length; k++)
                    strline[k] = Convert.ToString(line[k]);
                Field.frame.Add(strline);
            }
        }

        public static void GravityFunction()
        {
            while (true)
            {
                    Console.SetCursorPosition(Field.frame[1].Length, Field.frame.Count);
                    if (CountRock() == 1)
                    {
                        MoveRock1();

                    }
                    else if (CountRock() > 1)
                    {

                        MoveRock1();
                        for (int i = 0; i < 5; i++)
                            MoveRock2();

                    }
                    Thread.Sleep(200);
            }
        }
        public static void LivesFunction()
        {
            while (true)
            {
                for (int i = 0; i <= Field.frame.Count - 1; i++)
                {
                    for (int x = 0; x <= Field.frame[i].Length - 1; x++)
                    {
                        if (Field.frame[i][x] == Rock.value)
                        {
                            try
                            {
                                if (Field.frame[i + 1][x] == Hero.value)
                                {
                                    lives = lives - 1;
                                    Console.SetCursorPosition(1, 24);
                                    Console.Write("Lives: " + gameField.lives + "  ");
                                    if (lives == 0)
                                    {
                                        gameField.Defeat();
                                        Field.frame[i][x] = Empty.value;
                                        Field.frame[i + 1][x] = Rock.value;
                                        Console.SetCursorPosition(x, i);
                                        Console.Write(Empty.value);
                                        Console.SetCursorPosition(x, i + 1);
                                        Console.Write(Rock.value);
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
            
                    Thread.Sleep(200);
            }

        }
        public static void Renderer()
        {
            Console.Clear();
            for (int i = 0; i < Field.frame.Count; i++)
            {
                Console.WriteLine(string.Join("", Field.frame[i]));
            }
        }

        public static void MoveHero(ConsoleKeyInfo keyInfo)
        {
            bool stat = true;


            for (int i = Field.frame.Count - 1; i >= 0; i--)
            {
                for (int x = 0; x < Field.frame[i].Length; x++)
                {
                    if (Field.frame[i][x] == Hero.value)
                    {
                        if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
                        {
                            if (stat)
                            {
                                if ((i - 1) >= 0 && Field.frame[i - 1][x] == Diamong.value)
                                {
                                    CollectUp(ref i, ref x);
                                    stat = false;
                                }
                                else
                                {
                                    GoUp(ref i, ref x, ref stat);
                                }
                            }

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
                        if (keyInfo.Key == ConsoleKey.E)
                        {
                            if (stat) GoDig(ref i, ref x, ref stat);
                        }
                        if (keyInfo.Key == ConsoleKey.Q)
                        {
                            if (stat) GoDigL(ref i, ref x, ref stat);
                        }
                        if (keyInfo.Key == ConsoleKey.L)
                        {
                            gameField.score = gameField.maxpoint;
                            gameField.Defeat();
                        }
                        if (keyInfo.Key == ConsoleKey.K)
                        {
                            gameField.Win();
                        }
                        break;
                    }

                }
            }


        }
        public static void AddScores()
        {
            gameField.score += 100;
            if (gameField.score >= gameField.maxpoint) gameField.Win();
            Console.SetCursorPosition(12, 24);
            Console.Write("Score: " + gameField.score);
        }
        public static void GoUp(ref int i, ref int x, ref bool stat)
        {
            try
            {
                if ((i - 1) >= 0 && Field.frame[i - 1][x] == Sand.value || Field.frame[i - 1][x] == Empty.value)
                {
                    Field.frame[i][x] = Empty.value;
                    Field.frame[i - 1][x] = Hero.value;
                    Console.SetCursorPosition(x, i);
                    Console.Write(Empty.value);
                    Console.SetCursorPosition(x, i - 1);
                    Console.Write(Hero.value);
                    stat = false;
                    gameField.x = x;
                    gameField.y = i - 1;
                    Console.SetCursorPosition(40, 24);
                    Console.Write("Coordinates: x=" + gameField.x + ", y=" + gameField.y + " ");
                }

            }
            catch { }
            
        }
        public static void GoDown(ref int i, ref int x, ref bool stat)
        {
            if ((i + 1) <= (Field.frame.Count - 1) && Field.frame[i + 1][x] == Sand.value || Field.frame[i + 1][x] == Empty.value)
            {
                Field.frame[i][x] = Empty.value;
                Field.frame[i + 1][x] = Hero.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x, i + 1);
                Console.Write(Hero.value);
                stat = false;
                gameField.x = x;
                gameField.y = i + 1;
                Console.SetCursorPosition(40, 24);
                Console.Write("Coordinates: x=" + gameField.x + ", y=" + gameField.y + " ");
            }

        }
        public static void GoLeft(ref int i, ref int x, ref bool stat)
        {

            if ((x - 1) >= 0 && Field.frame[i][x - 1] == Sand.value || Field.frame[i][x - 1] == Empty.value)
            {
                Field.frame[i][x] = Empty.value;
                Field.frame[i][x - 1] = Hero.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x - 1, i);
                Console.Write(Hero.value);
                stat = false;
                gameField.x = x - 1;
                gameField.y = i;
                Console.SetCursorPosition(40, 24);
                Console.Write("Coordinates: x=" + gameField.x + ", y=" + gameField.y + " ");
            }
            else if ((x - 2) >= 0 && Field.frame[i][x - 2] == Empty.value && Field.frame[i][x - 1] == Rock.value)
            {
                Field.frame[i][x] = Empty.value;
                Field.frame[i][x - 1] = Hero.value;
                Field.frame[i][x - 2] = Rock.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x, i - 1);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x - 1, i);
                Console.Write(Hero.value);
                Console.SetCursorPosition(x - 2, i);
                Console.Write(Rock.value);
                stat = false;
                gameField.x = x - 1;
                gameField.y = i;
                Console.SetCursorPosition(40, 24);
                Console.Write("Coordinates: x=" + gameField.x + ", y=" + gameField.y + " ");
            }

        }
        public static void GoRight(ref int i, ref int x, ref bool stat)
        {
            if ((x - 1) <= (Field.frame[i].Length - 1) && Field.frame[i][x + 1] == Sand.value || Field.frame[i][x + 1] == Empty.value)
            {

                Field.frame[i][x] = Empty.value;
                Field.frame[i][x + 1] = Hero.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x + 1, i);
                Console.Write(Hero.value);
                stat = false;
                gameField.x = x + 1;
                gameField.y = i;
                Console.SetCursorPosition(40, 24);
                Console.Write("Coordinates: x=" + gameField.x + ", y=" + gameField.y + " ");
            }
            else if ((x - 2) <= (Field.frame[i].Length - 1) && Field.frame[i][x + 1] == Rock.value && Field.frame[i][x + 2] == Empty.value)
            {
                Field.frame[i][x] = Empty.value;
                Field.frame[i][x + 1] = Hero.value;
                Field.frame[i][x + 2] = Rock.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x + 1, i);
                Console.Write(Hero.value);
                Console.SetCursorPosition(x + 2, i);
                Console.Write(Rock.value);
                stat = false;
                gameField.x = x + 1;
                gameField.y = i;
                Console.SetCursorPosition(40, 24);
                Console.Write("Coordinates: x=" + gameField.x + ", y=" + gameField.y + " ");
            }

        }
        public static void GoDig(ref int i, ref int x, ref bool stat)
        {
            if ((x - 1) <= (Field.frame[i].Length - 1) && Field.frame[i][x + 1] == Sand.value)
            {
                Field.frame[i][x + 1] = Empty.value;
                Console.SetCursorPosition(x + 1, i);
                Console.Write(Empty.value);
                stat = false;
            }
        }
        public static void GoDigL(ref int i, ref int x, ref bool stat)
        {
            if ((x - 1) >= 0 && Field.frame[i][x - 1] == Sand.value)
            {
                Field.frame[i][x - 1] = Empty.value;
                Console.SetCursorPosition(x - 1, i);
                Console.Write(Empty.value);
                stat = false;
            }
        }
        public static void CollectUp(ref int i, ref int x)
        {
            if ((i - 1) >= 0 && Field.frame[i - 1][x] == Diamong.value)
            {
                Field.frame[i][x] = Empty.value;
                Field.frame[i - 1][x] = Hero.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x, i - 1);
                Console.Write(Hero.value);
                AddScores();
            }
            
        }
        public static void CollectDown(ref int i, ref int x)
        {
            if ((i + 1) <= (Field.frame.Count - 1) && Field.frame[i + 1][x] == Diamong.value)
            {
                Field.frame[i][x] = Empty.value;
                Field.frame[i + 1][x] = Hero.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x, i + 1);
                Console.Write(Hero.value);
                AddScores();
            }
        }
        public static void CollectLeft(ref int i, ref int x)
        {
            if ((x - 1) >= 0 && Field.frame[i][x - 1] == Diamong.value)
            {
                Field.frame[i][x] = Empty.value;
                Field.frame[i][x - 1] = Hero.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x - 1, i);
                Console.Write(Hero.value);
                AddScores();
            }
        }
        public static void CollectRight(ref int i, ref int x)
        {
            if ((x - 1) <= (Field.frame[i].Length - 1) && Field.frame[i][x + 1] == Diamong.value)
            {
                Field.frame[i][x] = Empty.value;
                Field.frame[i][x + 1] = Hero.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x + 1, i);
                Console.Write(Hero.value);
                AddScores();
            }
        }
        public static int CountRock()
        {
            int c = 0;
            for (int i = Field.frame.Count - 1; i >= 0; i--)
            {
                for (int x = Field.frame[i].Length - 1; x >= 0; x--)
                {
                    if (Field.frame[i][x] == Rock.value)
                    {
                        if (Field.frame[i + 1][x] == Empty.value)
                        {
                            c++;
                        }
                    }
                }
            }
            return c;
        }
        public static void MoveRock1()
        {
            for (int i = Field.frame.Count - 1; i >= 0; i--)
            {
                for (int x = Field.frame[i].Length - 1; x >= 0; x--)
                {
                    if (Field.frame[i][x] == Rock.value)
                    {
                       if (Field.frame[i + 1][x] == Empty.value)
                        {
                            Field.frame[i][x] = Empty.value;
                            Field.frame[i + 1][x] = Rock.value;
                            Console.SetCursorPosition(x, i);
                            Console.Write(Empty.value);
                            Console.SetCursorPosition(x, i + 1);
                            Console.Write(Rock.value);
                            return;
                        }
                    }
                }
            }
        }
        public static void BFS_step(int i1, int i2)
        {
            for (int i = 0; i < BFS_x.Count; i++)
            {
                if (BFS_x[i] == i1 && BFS_y[i] == i2)
                {
                    return;
                }
            }
            try
            {
                if (Field.frame[i1][i2] == Diamong.value)
                {
                    gameField.BFS_score = gameField.BFS_score + 100;
                    BFS_x.Add(i1);
                    BFS_y.Add(i2);
                    BFS_step(i1 + 1, i2);
                    BFS_step(i1 - 1, i2);
                    BFS_step(i1, i2 + 1);
                    BFS_step(i1, i2 - 1);
                }
                else if (Field.frame[i1][i2] == Hero.value || Field.frame[i1][i2] == Sand.value || Field.frame[i1][i2] == Empty.value)
                {
                    BFS_x.Add(i1);
                    BFS_y.Add(i2);
                    BFS_step(i1 + 1, i2);
                    BFS_step(i1 - 1, i2);
                    BFS_step(i1, i2 + 1);
                    BFS_step(i1, i2 - 1);
                }
                else
                {
                    BFS_x.Add(i1);
                    BFS_y.Add(i2);
                    return;
                }
            }
            catch { }
        }

        public static bool BFS(int i1, int i2)
        {
            BFS_x.Clear();
            BFS_y.Clear();
            gameField.BFS_score = 0 + gameField.score;
            BFS_step(i1, i2);
            if (gameField.BFS_score >= gameField.maxpoint)
            {
                return true;
            }
            return false;
        }

        public static int BFS_step_help(int i1, int i2, int distance)
        {
            distance++;
            int a = 1000, b = 1000, c = 1000, d = 1000;
            for (int i = 0; i < BFS_x_help.Count; i++)
            {
                if (BFS_x_help[i] == i1 && BFS_y_help[i] == i2)
                {
                    return 1000;
                }
            }

            if (Field.frame[i1][i2] == Diamong.value)
            {
                BFS_x_help.Add(i1);
                BFS_y_help.Add(i2);
                return distance;
            }
            else if (Field.frame[i1][i2] == Hero.value || Field.frame[i1][i2] == Sand.value || Field.frame[i1][i2] == Empty.value)
            {
                BFS_x_help.Add(i1);
                BFS_y_help.Add(i2);
                a = BFS_step_help(i1 + 1, i2, distance);
                b = BFS_step_help(i1 - 1, i2, distance);
                c = BFS_step_help(i1, i2 + 1, distance);
                d = BFS_step_help(i1, i2 - 1, distance);
            }
            else
            {
                BFS_x_help.Add(i1);
                BFS_y_help.Add(i2);
                return 1000;
            }
            return Math.Min(Math.Min(a, b), Math.Min(c, d));
        }

        public static int BFS_help(int i1, int i2)
        {
            BFS_x_help.Clear();
            BFS_y_help.Clear();
            int distance = BFS_step_help(i1, i2, -1);
            return distance;
        }


        public static void Random()
        {
            do
            {
                gameField.maxpoint = 0;
                int temp;
                for (int i = 1; i < Field.frame.Count - 1; i++)
                {
                    for (int x = 1; x < Field.frame[i].Length - 1; x++)
                    {
                        temp = rnd.Next() % 100;
                        if (temp < 70)
                        {
                            Field.frame[i][x] = Sand.value;
                        }
                        else if (temp < 80)
                        {
                            Field.frame[i][x] = Diamong.value;
                            maxpoint += 100;
                        }
                        else if (temp < 100)
                        {
                            Field.frame[i][x] = Rock.value;
                        }
                    }
                }
                Field.frame[1][1] = Hero.value;
                BFS_res = BFS(1, 1);
            }
            while (BFS_res == false);

        }
        public static void Random2()
        {
            int temp, bs = 0, bd = 0, br = 0;
            string prev = Sand.value;
            do
            {
                BFS_x.Clear();
                BFS_y.Clear();

                gameField.maxpoint = 0;
                bs = 0; bd = 0; br = 0;
                prev = Sand.value;
                for (int i = 1; i < Field.frame.Count - 1; i++)
                {
                    for (int x = 1; x < Field.frame[i].Length - 1; x++)
                    {
                        temp = rnd.Next() % 100;
                        if (prev == Sand.value)
                        {
                            bs = 10;
                            bd = 0;
                            br = 0;
                        }
                        else if (prev == Diamong.value)
                        {
                            bs = 0;
                            bd = 10;
                            br = 0;
                        }
                        else if (prev == Rock.value)
                        {
                            bs = 0;
                            bd = 0;
                            br = 10;
                        }
                        if (temp < (70 + bs - bd - br))
                        {
                            Field.frame[i][x] = Sand.value;
                            prev = Sand.value;
                        }
                        else if (temp < 80 + bs + bd - br)
                        {
                            Field.frame[i][x] = Diamong.value;
                            maxpoint += 100;
                            prev = Diamong.value;
                        }
                        else if (temp < 100 + bs + bd + br)
                        {
                            Field.frame[i][x] = Rock.value;
                            prev = Rock.value;
                        }
                    }
                }
                Field.frame[1][1] = Hero.value;
                BFS_res = BFS(1, 1);
            }
            while (BFS_res == false);

        }
        public static void MoveRock2()
        {
            for (int i = 0; i <= Field.frame.Count - 1; i++)
            {
                for (int x = 0; x <= Field.frame[i].Length - 1; x++)
                {
                    if (Field.frame[i][x] == Rock.value)
                    {
                        if (Field.frame[i + 1][x] == Empty.value)
                        {
                            Field.frame[i][x] = Empty.value;
                            Field.frame[i + 1][x] = Rock.value;
                            Console.SetCursorPosition(x, i);
                            Console.Write(Empty.value);
                            Console.SetCursorPosition(x, i + 1);
                            Console.Write(Rock.value);
                            return;
                        }
                    }
                }
            }
        }

    }
}
