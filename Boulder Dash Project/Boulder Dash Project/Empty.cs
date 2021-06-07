using System;

namespace Boulder_Dash_Project
{
    class Empty : Cell
    {
        public static char value = ' ';

        public override char Value { get => ' '; set => value = ' '; }

        /*public override string Value
        {
            get { return value; }
        }*/
    }
}
