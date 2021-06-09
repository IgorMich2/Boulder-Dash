using System;
using System.Collections.Generic;
using System.IO;

namespace Boulder_Dash_Project
{
    class Levels
    {
        public static bool Failedload = false;

        public static Cell ToCell(string symbol)
        {
            switch (symbol)
            {
                case "@":
                    return new Diamond();
                case "*":
                    return new Sand();
                case " ":
                    return new Empty();
                case "I":
                    return new Hero();
                case "o":
                    return new Rock();
            }
            return new TextCell(Convert.ToChar(symbol));
            throw new ArgumentException();
        }

        public static void GetArrayFromFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            int rowCount = lines.Length;
            int SizeOfLine = lines[0].Length;
            Failedload = false;

            for (int i = 0; i < rowCount; i++)
            {
                char[] line = lines[i].ToCharArray();
                if (line.Length != SizeOfLine)
                {
                    Failedload = true;
                    break;
                }
                List<Cell> Temp = new List<Cell>();
                for (int k = 0; k < line.Length; k++)
                {

                    string temp = Convert.ToString(line[k]);
                    Temp.Add(ToCell(temp));
                    if (Temp[k].Value == new Diamond().Value)
                    {
                        GameField.maxpoint = GameField.maxpoint + 100;
                    }
                }
                Field.frame.Add(Temp);
            }
        }

        public static void SaveToFile()
        {
            string writePath = "save.txt";

            using (StreamWriter sw = new StreamWriter(writePath))
            {
                for (int i = 0; i < Field.frame.Count; i++)
                {
                    for (int j = 0; j < Field.frame[0].Count; j++)
                    {
                        sw.Write(String.Format("{0}", Field.frame[i][j].Value));
                    }
                    sw.WriteLine("");
                }
                sw.Close();
            }

        }

        public static void Renderer()
        {
            for (int i = 0; i < Field.frame.Count; i++)
            {
                for (int j = 0; j < Field.frame[0].Count; j++)
                {
                    Output.PrintCellSimple(Field.frame[i][j]);
                }
                Output.PrintSeparator();
            }
        }
    }
}
