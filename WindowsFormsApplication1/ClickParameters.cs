using System;
using System.Windows.Forms;

namespace Bot
{
    [Serializable]
    public class ClickParameters
    {
        public int ID { get; set; }
        public System.Drawing.Point Point { get; set; }

        public MouseButtons Button { get; set; }
        public int Period { get; set; }

        public override string ToString()
        {
            return ID+"\t"+Button+"\t"+Point.X+"\t"+Point.Y+"\t"+Period;
        }
    }
}
