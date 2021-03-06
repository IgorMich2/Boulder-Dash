using System;
using System.Collections.Generic;
using System.Text;

namespace Boulder_Dash_Project
{
    class MovingRocks:Rock
    {
        public static void MoveRock1()
        {
            for (int y = Field.frame.Count - 2; y >= 0; y--)
            {
                for (int x = Field.frame[y].Count - 1; x >= 0; x--)
                {
                    if (Field.frame[y][x].Value == new Rock().Value)
                    {
                        if (Field.frame[y + 1][x].Value == new Empty().Value)
                        {
                            Field.frame[y][x] = new Empty();
                            Field.frame[y + 1][x] = new Rock();
                            Output.PrintCell(x, y, new Empty());
                            Output.PrintCell(x, y + 1, new Rock());
                            Output.LastPressedKey();
                            RocksDownGravity++;
                        }
                    }
                }
            }
        }

        public static void MoveRock2()
        {
            for (int y = 0; y < Field.frame.Count - 1; y++)
            {
                for (int x = 0; x <= Field.frame[y].Count - 1; x++)
                {
                    if (Field.frame[y][x].Value == new Rock().Value)
                    {
                        if (Field.frame[y + 1][x].Value == new Empty().Value)
                        {
                            Field.frame[y][x] = new Empty();
                            Field.frame[y + 1][x] = new Rock();
                            Output.PrintCell(x, y, new Empty());
                            Output.PrintCell(x, y + 1, new Rock());
                            Output.LastPressedKey();
                            RocksDownGravity++;
                        }
                    }
                }
            }
        }
    }
}
