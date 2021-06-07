using System;
using System.Collections.Generic;
using System.Text;

namespace Boulder_Dash_Project
{
    class NewCell : Cell
    {
        private char Text;
        public NewCell(char value)
        {
            this.Text = value;
        }

        public override char Value { get => Text; set => Text = value; }

    }
}
