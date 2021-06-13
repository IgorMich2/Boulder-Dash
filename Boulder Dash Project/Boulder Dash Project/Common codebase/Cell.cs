using System;

namespace Boulder_Dash_Project
{
    abstract class Cell
    {
        public virtual char Value { get; set; }
        public virtual bool CanEnter()
        {
            return false;
        }
        public virtual void OnEnter() { }
        public virtual string path()
        {
            return "x";
        }
    }
}
