using System;

namespace Boulder_Dash_Project
{
    class Rock : Cell
    {
        public override char Value { get => 'o';  }
        public static int RocksDownGravity = 0;
        public override string path()
        {
            return "rock.jpg";
        }
        public static int CountRock()
        {
            int count = 0;

            for (int y = Field.frame.Count - 2; y >= 0; y--)
            {
                for (int x = Field.frame[y].Count - 1; x >= 0; x--)
                {
                    if (Field.frame[y][x].Value == new Rock().Value)
                    {
                        if (Field.frame[y + 1][x].Value == new Empty().Value)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }  
    }
}
