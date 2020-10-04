using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    [Serializable]
    public class Settings
    {
        public BindingList<ClickParameters> moves = new BindingList<ClickParameters>();
        public int Period1 { get; set; }
        public int PeriodA { get; set; }
        public int PeriodB { get; set; }

        public bool Repeat { get; set; }
    }
}
