using System;
using System.Collections.Generic;

namespace Boulder_Dash_Project
{
    class GenerationLevel
    {
        public static Random rnd = new Random();
        public static bool BFS_res = false;
        public static int BFS_score;

        public static List<int> BFS_x = new List<int>();
        public static List<int> BFS_y = new List<int>();
        public static List<int> BFS_x_help = new List<int>();
        public static List<int> BFS_y_help = new List<int>();

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
                    BFS_score = BFS_score + 100;
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
            BFS_score = 0 + gameField.score;
            BFS_step(i1, i2);
            if (BFS_score >= gameField.maxpoint)
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
                            gameField.maxpoint += 100;
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
    }
}
