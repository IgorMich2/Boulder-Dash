using System;
using System.Collections.Generic;
using System.Text;

namespace Boulder_Dash_Project
{
    class Output
    {
        public static void PrintCell(int x, int y, Cell PrintedCell)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(PrintedCell.Value);
        }

        public static void PrintCellSimple(Cell PrintedCell)
        {
            Console.Write(PrintedCell.Value);
        }

        public static void PrintSeparator()
        {
            Console.WriteLine("");
        }

        public static void LastPressedKey()
        {
            Console.SetCursorPosition(40, 27);
            Console.Write("Last pressed key: ");
        }

        public static void BigSpace()
        {
            Console.Write("                     ");
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public static void SetToZero()
        {
            Console.SetCursorPosition(0, 0);
        }

        public static void Score()
        {
            Console.SetCursorPosition(1, 27);
            Console.Write("Score: " + GameField.score);
        }

        
        public static void Lives()
        {
            Console.SetCursorPosition(1, 26);
            Console.Write("Lives: " + Hero.lives);
        }

        public static ConsoleKeyInfo GetKey()
        {
            return Console.ReadKey();
        }

        public static void Deadlock()
        {
            Console.SetCursorPosition(1, 29);
            Console.Write("Deadlock: " + !GenerationLevel.DFS(Hero.y, Hero.x) + " ");
        }

        public static void Radar()
        {
            Console.SetCursorPosition(1, 28);
            Console.Write("Steps to @: " + GenerationLevel.BFSRadar(Hero.y, Hero.x));
            Output.BigSpace();
        }

        public static void Time()
        {
            Console.SetCursorPosition(1, 30);
            Console.Write("Time : " + DateTime.Now.Subtract(GameField.Time));
        }

        public static void Digs()
        {
            Console.SetCursorPosition(40, 29);
            Console.Write("Digs: " + Hero.digs + " ");
        }
        

        public static void SetColor()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        public static string GetName()
        {
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("Enter name");
            Console.SetCursorPosition(1, 2);
            string name = Console.ReadLine();
            Console.WriteLine("Thank you! Saving the result. Please wait...");
            return name;
        }

        public static void Exception(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        public static void Message(string text)
        {
            Console.WriteLine(text);
        }

        public static void Coordinates()
        {
            Console.SetCursorPosition(40, 28);
            Console.Write("Coordinates: x=" + Hero.x + ", y=" + Hero.y + " ");
        }

        public static void Steps()
        {
            Console.SetCursorPosition(40, 26);
            Console.Write("Steps: " + Hero.steps + " ");
        }
    }
}
