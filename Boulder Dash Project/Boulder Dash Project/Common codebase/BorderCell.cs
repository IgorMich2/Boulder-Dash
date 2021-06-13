using System;
using System.Collections.Generic;
using System.Text;

namespace Boulder_Dash_Project
{
    class BorderCell : Cell
    {
        public override bool CanEnter()
        {
            return false;
        }

        public override string path()
        {
            return "wall.jpg";
        }
        public BorderCell(char value)
        {
            this.Value = value;
        }
    }
}
