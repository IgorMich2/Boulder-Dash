using System;
using System.Collections.Generic;

namespace Boulder_Dash_Project
{
    class GenerationLevel
    {
        static Random randomNumber = new Random();

        static bool BFS_res = false;
        static int BFS_score;

        static List<int> DFSX = new List<int>();
        static List<int> DFSY = new List<int>();

        static List<(int, int, int)> BFSQuery = new List<(int, int, int)>();

        public static void DFSStep(int i1, int i2)
        {
            for (int i = 0; i < DFSX.Count; i++)
            {
                if (DFSX[i] == i1 && DFSY[i] == i2)
                {
                    return;
                }
            }
            try
            {
                if (Field.frame2[i1][i2].Value == new Diamond().Value)
                {
                    BFS_score = BFS_score + 100;
                    DFSX.Add(i1);
                    DFSY.Add(i2);
                    DFSStep(i1 + 1, i2);
                    DFSStep(i1 - 1, i2);
                    DFSStep(i1, i2 + 1);
                    DFSStep(i1, i2 - 1);
                }
                else if (Field.frame2[i1][i2].Value == new Hero().Value || Field.frame2[i1][i2].Value == new Sand().Value || Field.frame2[i1][i2].Value == new Empty().Value)
                {
                    DFSX.Add(i1);
                    DFSY.Add(i2);
                    DFSStep(i1 + 1, i2);
                    DFSStep(i1 - 1, i2);
                    DFSStep(i1, i2 + 1);
                    DFSStep(i1, i2 - 1);
                }
                else
                {
                    DFSX.Add(i1);
                    DFSY.Add(i2);
                    return;
                }
            }
            catch { }
        }

        public static bool DFS(int x0, int y0)
        {
            DFSX.Clear();
            DFSY.Clear();
            BFS_score = 0 + GameField.score;
            DFSStep(x0, y0);
            if (BFS_score >= GameField.maxpoint)
            {
                return true;
            }
            return false;
        }

        public static (int, int, int) BFSRadarStep((int, int, int) point2)
        {
            List<List<bool>> Visited = new List<List<bool>>();
            for (int i = 0; i < Field.frame2.Count; i++)
            {
                List<bool> Temp = new List<bool>();
                for (int j = 0; j < Field.frame2[0].Count; j++)
                {
                    Temp.Add(false);
                }
                Visited.Add(Temp);
            }

            BFSQuery.Add(point2);
            while(BFSQuery.Count != 0)
            {                
                if (Field.frame2[BFSQuery[0].Item1][BFSQuery[0].Item2].Value == new Diamond().Value)
                {
                    return (BFSQuery[0]);
                }
                
                if (Visited[BFSQuery[0].Item1+1][BFSQuery[0].Item2] == false && (Field.frame2[BFSQuery[0].Item1 + 1][BFSQuery[0].Item2].Value == new Diamond().Value || Field.frame2[BFSQuery[0].Item1 + 1][BFSQuery[0].Item2].Value == new Sand().Value || Field.frame2[BFSQuery[0].Item1 + 1][BFSQuery[0].Item2].Value == new Empty().Value))
                {
                    var tuple = (BFSQuery[0].Item1 + 1, BFSQuery[0].Item2, BFSQuery[0].Item3 + 1);
                    BFSQuery.Add(tuple);
                    Visited[BFSQuery[0].Item1 + 1][BFSQuery[0].Item2] = true;
                }
                if (Visited[BFSQuery[0].Item1 - 1][BFSQuery[0].Item2] == false && (Field.frame2[BFSQuery[0].Item1 - 1][BFSQuery[0].Item2].Value == new Diamond().Value || Field.frame2[BFSQuery[0].Item1 - 1][BFSQuery[0].Item2].Value == new Sand().Value || Field.frame2[BFSQuery[0].Item1 - 1][BFSQuery[0].Item2].Value == new Empty().Value))
                {
                    var tuple = (BFSQuery[0].Item1 - 1, BFSQuery[0].Item2, BFSQuery[0].Item3 + 1);
                    BFSQuery.Add(tuple);
                    Visited[BFSQuery[0].Item1 - 1][BFSQuery[0].Item2] = true;
                }
                if (Visited[BFSQuery[0].Item1][BFSQuery[0].Item2 + 1] == false && (Field.frame2[BFSQuery[0].Item1][BFSQuery[0].Item2+1].Value == new Diamond().Value || Field.frame2[BFSQuery[0].Item1][BFSQuery[0].Item2+1].Value == new Sand().Value || Field.frame2[BFSQuery[0].Item1][BFSQuery[0].Item2+1].Value == new Empty().Value))
                {
                    var tuple = (BFSQuery[0].Item1, BFSQuery[0].Item2 + 1, BFSQuery[0].Item3 + 1);
                    BFSQuery.Add(tuple);
                    Visited[BFSQuery[0].Item1][BFSQuery[0].Item2 + 1] = true;
                }
                if (Visited[BFSQuery[0].Item1][BFSQuery[0].Item2 - 1] == false && (Field.frame2[BFSQuery[0].Item1][BFSQuery[0].Item2 - 1].Value == new Diamond().Value || Field.frame2[BFSQuery[0].Item1][BFSQuery[0].Item2 - 1].Value == new Sand().Value || Field.frame2[BFSQuery[0].Item1][BFSQuery[0].Item2 - 1].Value == new Empty().Value))
                {
                    var tuple = (BFSQuery[0].Item1, BFSQuery[0].Item2 - 1, BFSQuery[0].Item3 + 1);
                    BFSQuery.Add(tuple);
                    Visited[BFSQuery[0].Item1][BFSQuery[0].Item2 - 1] = true;
                }
                BFSQuery.RemoveAt(0);
            }

            return (1000,1000, 1000);
        }

        public static int BFSRadar(int i1, int i2)
        {
            BFSQuery.Clear();

            (int, int, int) point2 = (i1, i2, 0);
            (int, int, int) distance = BFSRadarStep(point2);

            return distance.Item3;
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
                BFS_res = DFS(1, 1);
            }
            while (BFS_res == false);

        }
    }
}
