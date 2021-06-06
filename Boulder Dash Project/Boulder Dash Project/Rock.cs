using System;

namespace Boulder_Dash_Project
{
    class Rock : Cell
    {
        public static string value = "o";
        public static int RocksDownGravity = 0;


        /*public override string Value
        {
            get { return value; }
        }*/

        public static int CountRock()
        {
            int count = 0;

            for (int y = Field.frame.Count - 1; y >= 0; y--)
            {
                for (int x = Field.frame[y].Length - 1; x >= 0; x--)
                {
                    if (Field.frame[y][x] == Rock.value)
                    {
                        if (Field.frame[y + 1][x] == Empty.value)
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
            for (int y = Field.frame.Count - 1; y >= 0; y--)
            {
                for (int x = Field.frame[y].Length - 1; x >= 0; x--)
                {
                    if (Field.frame[y][x] == Rock.value)
                    {
                        if (Field.frame[y + 1][x] == Empty.value)
                        {
                            Field.frame[y][x] = Empty.value;
                            Field.frame[y + 1][x] = Rock.value;
                            Console.SetCursorPosition(x, y);
                            Console.Write(Empty.value);
                            Console.SetCursorPosition(x, y + 1);
                            Console.Write(Rock.value);
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
