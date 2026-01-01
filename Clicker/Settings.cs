using System;
using System.ComponentModel;

namespace Clicker
{
    [Serializable]
    public class Settings
    {
        public BindingList<ClickParameters> Moves { get; set; } = new BindingList<ClickParameters>();
        public int Period1 { get; set; }
        public int PeriodA { get; set; }
        public int PeriodB { get; set; }

        public bool Repeat { get; set; }
        public int NumberOfRepeats { get; set; }
    }
}
