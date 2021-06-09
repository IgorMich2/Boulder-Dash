using System;
using System.Collections.Generic;

namespace Boulder_Dash_Project
{
    class GenerationLevel
    {
        static Random randomNumber = new Random();

        static bool BFSResult = false;
        public static int BFSScore = 0;

        public static (int, int, int) point;

        static List<int> DFSX = new List<int>();
        static List<int> DFSY = new List<int>();

        public static List<List<bool>> Visited = new List<List<bool>>();

        static List<(int y , int x , int distance)> BFSQuery = new List<(int y, int x , int distance)>();

        public static void DFSStep(int i1, int i2)
        {
            for (int i = 0; i < DFSX.Count; i++)
            {
                if (DFSX[i] == i1 && DFSY[i] == i2)
                {
                    return;
                }
            }

            if (i1 > 0 && i2 > 0 && i1 < Field.frame.Count - 1 && i2 < Field.frame[0].Count - 1)
            {
                DFSX.Add(i1);
                DFSY.Add(i2);
                if (Field.frame[i1][i2].CanEnter() == true)
                {
                    if (Field.frame[i1][i2].Value == new Diamond().Value)
                    {
                        BFSScore = BFSScore + 100;
                    }
                    DFSStep(i1 + 1, i2);
                    DFSStep(i1 - 1, i2);
                    DFSStep(i1, i2 + 1);
                    DFSStep(i1, i2 - 1);
                }
                return;
            }
        }

        public static bool DFS(int x0, int y0)
        {
            DFSX.Clear();
            DFSY.Clear();
            BFSScore = 0 + GameField.score;
            DFSStep(x0, y0);
            if (BFSScore >= GameField.maxpoint)
            {
                return true;
            }
            return false;
        }

        public static void AddToQuery(int dy, int dx)
        {
            if (Visited[BFSQuery[0].y + dy][BFSQuery[0].x + dx] == false && (Field.frame[BFSQuery[0].y + dy][BFSQuery[0].x + dx].CanEnter() == true))
            {
                var tuple = (BFSQuery[0].y + dy, BFSQuery[0].x + dx, BFSQuery[0].distance + 1);
                BFSQuery.Add(tuple);
                Visited[BFSQuery[0].y + dy][BFSQuery[0].x + dx] = true;
            }
        }

        public static int BFSRadarStep((int, int, int) point)
        {
            Visited.Clear();
            for (int i = 0; i < Field.frame.Count; i++)
            {
                List<bool> Temp = new List<bool>();
                for (int j = 0; j < Field.frame[0].Count; j++)
                {
                    Temp.Add(false);
                }
                Visited.Add(Temp);
            }

            BFSQuery.Add(point);
            while (BFSQuery.Count != 0)
            {
                if (Field.frame[BFSQuery[0].y][BFSQuery[0].x].Value == new Diamond().Value)
                {
                    return (BFSQuery[0].distance);
                }

                if (BFSQuery[0].y > 0 && BFSQuery[0].x > 0) 
                {
                    AddToQuery(1, 0);
                    AddToQuery(0, 1);
                    AddToQuery(-1, 0);
                    AddToQuery(0, -1);
                }

                BFSQuery.RemoveAt(0);
            }

            return (1000);
        }

        public static int BFSRadar(int y, int x)
        {
            BFSQuery.Clear();
            
            point = (y, x, 0);
            int distance = BFSRadarStep(point);

            return distance;
        }

        public static void Random2()
        {
            int temp, bs = 0, bd = 0, br = 0;
            Cell prev = new Sand();
            do
            {
                DFSX.Clear();
                DFSY.Clear();

                GameField.maxpoint = 0;
                bs = 0; bd = 0; br = 0;
                prev = new Sand();
                for (int i = 1; i < Field.frame.Count - 1; i++)
                {
                    for (int x = 1; x < Field.frame[i].Count - 1; x++)
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
                            Field.frame[i][x] = new Sand();
                            prev = new Sand();
                        }
                        else if (temp < 80 + bs + bd - br)
                        {
                            Field.frame[i][x] = new Diamond();
                            GameField.maxpoint += 100;
                            prev = new Diamond();
                        }
                        else if (temp < 100 + bs + bd + br)
                        {
                            Field.frame[i][x] = new Rock();
                            prev = new Rock();
                        }
                    }
                }
                Field.frame[1][1] = new Hero();
                BFSResult = DFS(1, 1);
            }
            while (BFSResult == false);

        }

        public static void Intellectual()
        {
            int temp, bs = 0, bd = 0, br = 0;
            char prev = new Sand().Value;
            do
            {
                DFSX.Clear();
                DFSY.Clear();

                GameField.maxpoint = 0;
                bs = 0; bd = 0; br = 0;
                prev = new Sand().Value;
                for (int i = 1; i < Field.frame.Count - 1; i++)
                {
                    for (int x = 1; x < Field.frame[i].Count - 1; x++)
                    {
                        temp = randomNumber.Next() % 100;
                        if (prev == new Sand().Value)
                        {
                            bs = 10;
                            bd = 0;
                            br = 0;
                        }
                        else if (prev == new Diamond().Value)
                        {
                            bs = 0;
                            bd = 10;
                            br = 0;
                        }
                        else if (prev == new Rock().Value)
                        {
                            bs = 0;
                            bd = 0;
                            br = 10;
                        }
                        if (temp < (70 + bs - bd - br))
                        {
                            Field.frame[i][x] = new Sand();
                            prev = new Sand().Value;
                        }
                        else if (temp < 80 + bs + bd - br)
                        {
                            Field.frame[i][x] = new Diamond();
                            GameField.maxpoint += 100;
                            prev = new Diamond().Value;
                        }
                        else if (temp < 100 + bs + bd + br)
                        {
                            Field.frame[i][x] = new Rock();
                            prev = new Rock().Value;
                        }
                    }
                }


                int h = randomNumber.Next() % 12, w = randomNumber.Next() % 14, num;
                for (int i = 0; i < h; i++)
                {
                    num = randomNumber.Next() % 14;
                    if (num < 3)
                    {
                        num = num + 3;
                    }
                    for (int x = 1; x < Field.frame[0].Count - 1; x++)
                    {
                        temp = randomNumber.Next() % 100;
                        prev = new Rock().Value;
                        if (prev == new Sand().Value)
                        {
                            bs = 10;
                            br = 0;
                        }
                        else if (prev == new Rock().Value)
                        {
                            bs = 0;
                            br = 10;
                        }
                        if (temp < 70 + br - bs)
                        {
                            if (Field.frame[num][x].Value == new Diamond().Value)
                            {
                                GameField.maxpoint = GameField.maxpoint - 100;
                            }
                            Field.frame[num][x] = new Rock();
                            prev = new Rock().Value;
                        }
                        else
                        {
                            if (Field.frame[num][x].Value == new Diamond().Value)
                            {
                                GameField.maxpoint = GameField.maxpoint - 100;
                            }
                            Field.frame[num][x] = new Sand();
                            prev = new Sand().Value;
                        }
                    }
                }
                for (int i = 0; i < w; i++)
                {
                    num = randomNumber.Next() % 77;
                    if (num < 3)
                    {
                        num = num + 3;
                    }
                    for (int x = 1; x < Field.frame.Count - 1; x++)
                    {
                        temp = randomNumber.Next() % 100;
                        prev = new Rock().Value;
                        if (prev == new Sand().Value)
                        {
                            bs = 10;
                            br = 0;
                        }
                        else if (prev == new Rock().Value)
                        {
                            bs = 0;
                            br = 10;
                        }
                        if (temp < 70 + br - bs)
                        {
                            if (Field.frame[x][num].Value == new Diamond().Value)
                            {
                                GameField.maxpoint = GameField.maxpoint - 100;
                            }
                            Field.frame[x][num] = new Rock();
                            prev = new Rock().Value;
                        }
                        else
                        {
                            if (Field.frame[x][num].Value == new Diamond().Value)
                            {
                                GameField.maxpoint = GameField.maxpoint - 100;
                            }
                            Field.frame[x][num] = new Sand();
                            prev = new Sand().Value;
                        }
                    }
                }


                Field.frame[1][1] = new Hero();
                BFSResult = DFS(1, 1);
            }
            while (BFSResult == false);
            //gameField.Renderer(Boulder);
        }
    }
}
