using System;
namespace Boulder_Dash_Project
{
    class Sand : Cell
    {
        public override char Value { get => '*'; }
        public override bool CanEnter()
        {
            return true;
        }
    }
}
