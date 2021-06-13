using System;

namespace Boulder_Dash_Project
{
    class Diamond : Cell
    {
        public override char Value { get => '@'; }
        public override bool CanEnter()
        {
            return true;
        }
        public override void OnEnter()
        {
            GameField.AddScores();
        }

        public override string path()
        {
            return "diamond.jpg";
        }
    }
}
