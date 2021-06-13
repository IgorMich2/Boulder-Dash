using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Boulder_Dash_Project
{
    class Gravity
    {
        public static void GravityFunction()
        {
            while (true)
            {
                if (GameField.GameStatus)
                {
                    if (Rock.CountRock() == 1)
                    {
                        MovingRocks.MoveRock1();
                    }
                    else if (Rock.CountRock() > 1)
                    {
                        MovingRocks.MoveRock1();
                        for (int i = 0; i < 5; i++)
                            MovingRocks.MoveRock2();
                    }
                }
                Thread.Sleep(200);
            }
        }
    }
}
