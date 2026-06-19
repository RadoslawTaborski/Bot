namespace Clicker
{
    public class MouseAction : Action
    {
        public MouseActions Button { get; set; }
        public System.Drawing.Point Point { get; set; }

        public override string ToString()
        {
            return Id + ". " + Button + " ; " + Period + " ; " + Point.X + " ; " + Point.Y;
        }
    }
}
