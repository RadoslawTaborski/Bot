using System;

namespace Clicker
{
    [Serializable]
    public class Parameters
    {
        public int Id { get; set; }
        public System.Drawing.Point Point { get; set; }
        public string Text { get; set; }

        public Actions Action { get; set; }
        public int Period { get; set; }

        public override string ToString()
        {
            if (Action == Actions.Keyboard)
            {
                return Id + ": " + Action + " ; " + Period + " ; " + Text;
            }
            return Id+". "+Action+" ; "+Period+" ; "+Point.X+" ; "+Point.Y;
        }
    }
}
