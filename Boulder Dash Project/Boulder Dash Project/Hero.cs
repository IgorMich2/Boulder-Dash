using System;
using System.Collections.Generic;
using System.Text;

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
                                    Hero.CollectUp(ref i, ref x);
                                    stat = false;
                                }
                                else
                                {
                                    Hero.GoUp(ref i, ref x, ref stat);
                                }
                            }

                        }
                        if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
                        {
                            if (stat) Hero.GoDown(ref i, ref x, ref stat);
                            Hero.CollectDown(ref i, ref x);
                        }
                        if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
                        {
                            if (stat) Hero.GoLeft(ref i, ref x, ref stat);
                            Hero.CollectLeft(ref i, ref x);
                        }
                        if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
                        {
                            if (stat) Hero.GoRight(ref i, ref x, ref stat);
                            Hero.CollectRight(ref i, ref x);
                        }
                        if (keyInfo.Key == ConsoleKey.E)
                        {
                            if (stat) Hero.GoDig(ref i, ref x, ref stat);
                        }
                        if (keyInfo.Key == ConsoleKey.Q)
                        {
                            if (stat) Hero.GoDigL(ref i, ref x, ref stat);
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
                gameField.AddScores();
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
                gameField.AddScores();
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
                gameField.AddScores();
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
                gameField.AddScores();
            }
        }

    }
}
