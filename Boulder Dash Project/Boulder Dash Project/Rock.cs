using System;

namespace Boulder_Dash_Project
{
    class Rock : Ceil
    {
        public static string value = "o";

        public override string Value
        {
            get { return value; }
        }

        public static int CountRock()
        {
            int count = 0;

            for (int i = Field.frame.Count - 1; i >= 0; i--)
            {
                for (int x = Field.frame[i].Length - 1; x >= 0; x--)
                {
                    if (Field.frame[i][x] == Rock.value)
                    {
                        if (Field.frame[i + 1][x] == Empty.value)
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
