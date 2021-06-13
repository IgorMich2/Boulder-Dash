using System;
using System.Collections.Generic;
using System.Text;

namespace Boulder_Dash_Project
{
    class TextCell : Cell
    {
        public override string path()
        {
            return (Convert.ToString(Value) + ".png");
        }
        public TextCell(char value)
        {
            this.Value = value;
        }
    }
}
