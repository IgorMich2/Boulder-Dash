using System;
using System.Collections.Generic;

namespace Boulder_Dash_Project
{
    class GenerationLevel
    {
        static Random randomNumber = new Random();

        static bool BFS_res = false;
        static int BFS_score;

        static List<int> BFS_x = new List<int>();
        static List<int> BFS_y = new List<int>();
        static List<int> BFS_x_help = new List<int>();
        static List<int> BFS_y_help = new List<int>();

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
                if (Field.frame2[i1][i2].Value == new Diamond().Value)
                {
                    BFS_score = BFS_score + 100;
                    BFS_x.Add(i1);
                    BFS_y.Add(i2);
                    BFS_step(i1 + 1, i2);
                    BFS_step(i1 - 1, i2);
                    BFS_step(i1, i2 + 1);
                    BFS_step(i1, i2 - 1);
                }
                else if (Field.frame2[i1][i2].Value == new Hero().Value || Field.frame2[i1][i2].Value == new Sand().Value || Field.frame2[i1][i2].Value == new Empty().Value)
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

        public static bool BFS(int x0, int y0)
        {
            BFS_x.Clear();
            BFS_y.Clear();
            BFS_score = 0 + GameField.score;
            BFS_step(x0, y0);
            if (BFS_score >= GameField.maxpoint)
            {
                return true;
            }
            return false;
        }

        public static int BFS_step_help(int i1, int i2, int distance)
        {
            distance++;
            if (distance > 8)
            {
                return 1000;
            }
            int a = 1000, b = 1000, c = 1000, d = 1000;
            for (int i = 0; i < BFS_x_help.Count; i++)
            {
                if (BFS_x_help[i] == i1 && BFS_y_help[i] == i2)
                {
                    return 1000;
                }
            }
            try
            {
                if (Field.frame2[i1][i2] == new Diamond())
                {
                    BFS_x_help.Add(i1);
                    BFS_y_help.Add(i2);
                    return distance;
                }
                else if (Field.frame2[i1][i2].Value == new Hero().Value || Field.frame2[i1][i2].Value == new Sand().Value || Field.frame2[i1][i2].Value == new Empty().Value)
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
            }
            catch { }
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
            Cell prev = new Sand();
            do
            {
                BFS_x.Clear();
                BFS_y.Clear();

                GameField.maxpoint = 0;
                bs = 0; bd = 0; br = 0;
                prev = new Sand();
                for (int i = 1; i < Field.frame2.Count - 1; i++)
                {
                    for (int x = 1; x < Field.frame2[i].Count - 1; x++)
                    {
                        temp = randomNumber.Next() % 100;
                        if (prev.Value == new Sand().Value)
                        {
                            bs = 10;
                            bd = 0;
                            br = 0;
                        }
                        else if (prev.Value == new Diamond().Value)
                        {
                            bs = 0;
                            bd = 10;
                            br = 0;
                        }
                        else if (prev.Value == new Rock().Value)
                        {
                            bs = 0;
                            bd = 0;
                            br = 10;
                        }
                        if (temp < (70 + bs - bd - br))
                        {
                            Field.frame2[i][x] = new Sand();
                            prev = new Sand();
                        }
                        else if (temp < 80 + bs + bd - br)
                        {
                            Field.frame2[i][x] = new Diamond();
                            GameField.maxpoint += 100;
                            prev = new Diamond();
                        }
                        else if (temp < 100 + bs + bd + br)
                        {
                            Field.frame2[i][x] = new Rock();
                            prev = new Rock();
                        }
                    }
                }
                Field.frame2[1][1] = new Hero();
                BFS_res = BFS(1, 1);
            }
            while (BFS_res == false);

        }
    }
}
