using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Boulder_Dash_Project
{
    class Lives
    {
        public static void LivesFunction()
        {
            while (true)
            {
                try
                {
                    if (Hero.y > 0 && Hero.lives > 0 && GameField.GameStatus == true)
                    {
                        if (Field.frame[Hero.y - 1][Hero.x].Value == new Rock().Value)
                        {
                            Hero.lives = Hero.lives - 1;
                            Logic.Lives();
                            Logic.BigSpace();
                            if (Hero.lives <= 0)
                            {
                                GameField.Defeat();
                            }
                        }
                    }
                }
                catch { }
                Thread.Sleep(200);
            }
        }
    }
}
