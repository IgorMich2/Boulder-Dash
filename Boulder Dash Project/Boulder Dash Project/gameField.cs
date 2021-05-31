using System;
using System.Threading;
using System.IO;

namespace Boulder_Dash_Project
{
    class GameField : Field
    {
        public static int score = 0;
        public static int maxpoint = 0;
        public static int lives = 500;

        public static int x;
        public static int y;

        public static void Win()
        {
            Field.player.SoundLocation = "win.wav";
            Field.player.Play();

            Console.Clear();
            Console.WriteLine("Win!");
            Thread.Sleep(5000);
            Field.player.Stop();
            GameField.score = GameField.maxpoint;
            Console.Clear();

        }

        public static void Defeat()
        {
            Console.Clear();
            Field.player.Stop();
            Field.player.SoundLocation = "loose.wav";
            Field.player.Play();

            Console.WriteLine("Defeat!");
            Thread.Sleep(3000);
            Field.player.Stop();
            GameField.score = GameField.maxpoint;
        }

        public static void GetArrayFromFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            int rowCount = lines.Length;

            for (int i = 0; i < rowCount - 1; i++)
            {
                char[] line = lines[i + 1].ToCharArray();
                string[] strline = new string[line.Length];
                for (int k = 0; k < line.Length; k++)
                    strline[k] = Convert.ToString(line[k]);
                Field.frame.Add(strline);
            }
        }

        public static void GravityFunction()
        {
            while (true)
            {
                Console.SetCursorPosition(Field.frame[1].Length, Field.frame.Count);
                if (Rock.CountRock() == 1)
                {
                    Rock.MoveRock1();

                }
                else if (Rock.CountRock() > 1)
                {

                    Rock.MoveRock1();
                    for (int i = 0; i < 5; i++)
                        Rock.MoveRock2();

                }
                Thread.Sleep(200);
            }
        }
        public static void LivesFunction()
        {
            while (true)
            {
                for (int i = 0; i <= Field.frame.Count - 1; i++)
                {
                    for (int x = 0; x <= Field.frame[i].Length - 1; x++)
                    {
                        if (Field.frame[i][x] == Rock.value)
                        {
                            try
                            {
                                if (Field.frame[i + 1][x] == Hero.value)
                                {
                                    lives = lives - 1;
                                    Console.SetCursorPosition(1, 24);
                                    Console.Write("Lives: " + GameField.lives + "  ");
                                    if (lives == 0)
                                    {
                                        GameField.Defeat();
                                        Field.frame[i][x] = Empty.value;
                                        Field.frame[i + 1][x] = Rock.value;
                                        Console.SetCursorPosition(x, i);
                                        Console.Write(Empty.value);
                                        Console.SetCursorPosition(x, i + 1);
                                        Console.Write(Rock.value);
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }

                Thread.Sleep(200);
            }

        }
        public static void Renderer()
        {
            Console.Clear();
            for (int i = 0; i < Field.frame.Count; i++)
            {
                Console.WriteLine(string.Join("", Field.frame[i]));
            }
        }

        
        public static void AddScores()
        {
            GameField.score += 100;
            if (GameField.score >= GameField.maxpoint) GameField.Win();
            Console.SetCursorPosition(12, 24);
            Console.Write("Score: " + GameField.score);
        }
        

    }
}