using System;

namespace Boulder_Dash_Project
{
    class Rock : Cell
    {
        public override char Value { get => 'o'; set => value = 'o'; }
        public static int RocksDownGravity = 0;




        /*public override string Value
        {
            get { return value; }
        }*/

        public static int CountRock()
        {
            int count = 0;

            for (int y = Field.frame2.Count - 1; y >= 0; y--)
            {
                for (int x = Field.frame2[y].Count - 1; x >= 0; x--)
                {
                    if (Field.frame2[y][x].Value == new Rock().Value)
                    {
                        if (Field.frame2[y + 1][x].Value == new Empty().Value)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        public static void MoveRock1()
        {
            for (int y = Field.frame2.Count - 1; y >= 0; y--)
            {
                for (int x = Field.frame2[y].Count - 1; x >= 0; x--)
                {
                    if (Field.frame2[y][x].Value == new Rock().Value)
                    {
                        if (Field.frame2[y + 1][x].Value == new Empty().Value)
                        {
                            Field.frame2[y][x] = new Empty();
                            Field.frame2[y + 1][x] = new Rock();
                            Console.SetCursorPosition(x, y);
                            Console.Write(new Empty().Value);
                            Console.SetCursorPosition(x, y + 1);
                            Console.Write(new Rock().Value);
                            RocksDownGravity++;
                            Console.SetCursorPosition(40, 25);
                            Console.Write("Last pressed key: ");
                        }
                    }
                }
            }
        }

        public static void MoveRock2()
        {
            for (int i = 0; i <= Field.frame2.Count - 1; i++)
            {
                for (int x = 0; x <= Field.frame2[i].Count - 1; x++)
                {
                    if (Field.frame2[i][x].Value == new Rock().Value)
                    {
                        if (Field.frame2[i + 1][x].Value == new Empty().Value)
                        {
                            Field.frame2[i][x] = new Empty();
                            Field.frame2[i + 1][x] = new Rock();
                            Console.SetCursorPosition(x, i);
                            Console.Write(new Empty().Value);
                            Console.SetCursorPosition(x, i + 1);
                            Console.Write(new Rock().Value);
                            Console.SetCursorPosition(40, 25);
                            Console.Write("Last pressed key: ");
                            RocksDownGravity++;
                        }
                    }
                }
            }
        }

    }
}
