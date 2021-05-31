using System;

namespace Boulder_Dash_Project
{
    class Hero : Ceil
    {
        public static string value = "I";
        public override string Value
        {
            get { return value; }
        }

        public static void MoveHero(ConsoleKeyInfo keyInfo)
        {
            for (int i = Field.frame.Count - 1; i >= 0; i--)
            {
                for (int x = 0; x < Field.frame[i].Length; x++)
                {
                    if (Field.frame[i][x] == Hero.value)
                    {
                        if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
                        {
                            Hero.GoUp(ref i, ref x);
                        }
                        else if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
                        {
                            Hero.GoDown(ref i, ref x);
                        }
                        else if(keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
                        {
                            Hero.GoLeft(ref i, ref x);
                        }
                        else if(keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
                        {
                            Hero.GoRight(ref i, ref x);
                        }
                        else if(keyInfo.Key == ConsoleKey.E)
                        {
                            Hero.GoDig(ref i, ref x);
                        }
                        else if(keyInfo.Key == ConsoleKey.Q)
                        {
                            Hero.GoDigL(ref i, ref x);
                        }
                        else if(keyInfo.Key == ConsoleKey.L)
                        {
                            GameField.score = GameField.maxpoint;
                            GameField.Defeat();
                        }
                        else if(keyInfo.Key == ConsoleKey.K)
                        {
                            GameField.Win();
                        }
                        i = -1;
                        break;
                    }
                }
            }
            Console.SetCursorPosition(40, 25);
            Console.Write("Coordinates: x=" + GameField.x + ", y=" + GameField.y + " ");
        }

        public static void GoUp(ref int i, ref int x)
        {
            try
            {
                if ((i - 1) >= 0 && (Field.frame[i - 1][x] == Sand.value || Field.frame[i - 1][x] == Empty.value))
                {
                    Field.frame[i][x] = Empty.value;
                    Field.frame[i - 1][x] = Hero.value;
                    Console.SetCursorPosition(x, i);
                    Console.Write(Empty.value);
                    Console.SetCursorPosition(x, i - 1);
                    Console.Write(Hero.value);
                    GameField.x = x;
                    GameField.y = i - 1;

                }
                else if ((i - 1) >= 0 && Field.frame[i - 1][x] == Diamong.value)
                {
                    Field.frame[i][x] = Empty.value;
                    Field.frame[i - 1][x] = Hero.value;
                    Console.SetCursorPosition(x, i);
                    Console.Write(Empty.value);
                    Console.SetCursorPosition(x, i - 1);
                    Console.Write(Hero.value);
                    GameField.AddScores();
                }
            }
            catch { }

        }
        public static void GoDown(ref int i, ref int x)
        {
            if ((i + 1) <= (Field.frame.Count - 1) && Field.frame[i + 1][x] == Sand.value || Field.frame[i + 1][x] == Empty.value)
            {
                Field.frame[i][x] = Empty.value;
                Field.frame[i + 1][x] = Hero.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x, i + 1);
                Console.Write(Hero.value);
                GameField.x = x;
                GameField.y = i + 1;

            }
            else if ((i + 1) <= (Field.frame.Count - 1) && Field.frame[i + 1][x] == Diamong.value)
            {
                Field.frame[i][x] = Empty.value;
                Field.frame[i + 1][x] = Hero.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x, i + 1);
                Console.Write(Hero.value);
                GameField.AddScores();
            }

        }
        public static void GoLeft(ref int i, ref int x)
        {

            if ((x - 1) >= 0 && Field.frame[i][x - 1] == Sand.value || Field.frame[i][x - 1] == Empty.value)
            {
                Field.frame[i][x] = Empty.value;
                Field.frame[i][x - 1] = Hero.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x - 1, i);
                Console.Write(Hero.value);
                GameField.x = x - 1;
                GameField.y = i;
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
                GameField.x = x - 1;
                GameField.y = i;
            }
            else if ((x - 1) >= 0 && Field.frame[i][x - 1] == Diamong.value)
            {
                Field.frame[i][x] = Empty.value;
                Field.frame[i][x - 1] = Hero.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x - 1, i);
                Console.Write(Hero.value);
                GameField.AddScores();
            }

        }
        public static void GoRight(ref int i, ref int x)
        {
            if ((x - 1) <= (Field.frame[i].Length - 1) && Field.frame[i][x + 1] == Sand.value || Field.frame[i][x + 1] == Empty.value)
            {

                Field.frame[i][x] = Empty.value;
                Field.frame[i][x + 1] = Hero.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x + 1, i);
                Console.Write(Hero.value);
                GameField.x = x + 1;
                GameField.y = i;

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
                GameField.x = x + 1;
                GameField.y = i;

            }
            else if ((x - 1) <= (Field.frame[i].Length - 1) && Field.frame[i][x + 1] == Diamong.value)
            {
                Field.frame[i][x] = Empty.value;
                Field.frame[i][x + 1] = Hero.value;
                Console.SetCursorPosition(x, i);
                Console.Write(Empty.value);
                Console.SetCursorPosition(x + 1, i);
                Console.Write(Hero.value);
                GameField.AddScores();
            }

        }
        public static void GoDig(ref int i, ref int x)
        {
            if ((x - 1) <= (Field.frame[i].Length - 1) && Field.frame[i][x + 1] == Sand.value)
            {
                Field.frame[i][x + 1] = Empty.value;
                Console.SetCursorPosition(x + 1, i);
                Console.Write(Empty.value);
            }
        }
        public static void GoDigL(ref int i, ref int x)
        {
            if ((x - 1) >= 0 && Field.frame[i][x - 1] == Sand.value)
            {
                Field.frame[i][x - 1] = Empty.value;
                Console.SetCursorPosition(x - 1, i);
                Console.Write(Empty.value);
            }
        }
        

    }
}
