using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Boulder_Dash_Project
{
    class LevelEditor
    {
        public static (int x, int y) cursor = (1, 1);

        public static void ChangeCell()
        {
            switch (Field.frame[cursor.y][cursor.x].Value)
            {
                case '@':
                    {
                        Field.frame[cursor.y][cursor.x] = new Empty();
                        break;
                    }
                case ' ':
                    {
                        Field.frame[cursor.y][cursor.x] = new Hero();
                        break;
                    }
                case 'I':
                    {
                        Field.frame[cursor.y][cursor.x] = new Rock();
                        break;
                    }
                case 'o':
                    {
                        Field.frame[cursor.y][cursor.x] = new Sand();
                        break;
                    }
                case '*':
                    {
                        Field.frame[cursor.y][cursor.x] = new Diamond();
                        break;
                    }
            }


            string writePath = "5.txt";

            using (StreamWriter sw = new StreamWriter(writePath))
            {
                for (int j = 0; j < Field.frame.Count; j++)
                {
                    for (int i = 0; i < Field.frame[0].Count; i++)
                    {
                        sw.Write(Field.frame[j][i].Value);
                    }
                    sw.WriteLine("");
                }
            }
            Console.Clear();
            Field.frame.Clear();
            Levels.GetArrayFromFile("5.txt");
            Output.Renderer();
            Console.SetCursorPosition(cursor.x, cursor.y);
        }

        public static void Start()
        {
            Console.SetCursorPosition(1, 1);
        }

        public static void MoveCursor(int dx, int dy)
        {
            if ((cursor.x + dx) > 0 && (cursor.y + dy) > 0 && (cursor.y + dy) < (GameField.frame.Count - 1) && (cursor.x + dx) < (GameField.frame[0].Count - 1))
            {
                cursor.x += dx;
                cursor.y += dy;
            }
            Console.Clear();
            Field.frame.Clear();
            Levels.GetArrayFromFile("5.txt");
            Output.Renderer();
            Console.SetCursorPosition(cursor.x, cursor.y);
        }
    }
}
