using System;
using NAudio.Wave;
using System.Threading;
using System.Windows.Forms;
namespace Boulder_Dash_Project
{
    class Hero : Cell
    {
        public static int lives = 500;
        public static int x;
        public static int y;
        public static int steps=0;
        public static int digs=0;
        public static string value = "I";
        public static int RocksMoveByHero = 0;

        public override string path()
        {
            return "hero.jpg";
        }

        public override char Value { get => 'I'; }
        public override bool CanEnter()
        {
            return true;
        }

        public static void FindHero()
        {
            for (int y = 0; y < Field.frame.Count; y++)
            {
                for (int x = 0; x < Field.frame[y].Count; x++)
                {
                    if (Field.frame[y][x].Value == new Hero().Value)
                    {
                        Hero.x = x;
                        Hero.y = y;
                        y = 10000;
                        break;
                    }
                }
            }
        }

        
    }
}
