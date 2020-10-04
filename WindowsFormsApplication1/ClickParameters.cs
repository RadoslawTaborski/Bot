using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
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
