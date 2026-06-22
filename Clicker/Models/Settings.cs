using Clicker.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace Clicker
{
    public class Settings
    {
        public BindingList<Action> Moves { get; set; } = new BindingList<Action>();
        public List<TagSetting> Tags { get; set; } = new List<TagSetting>();
        public Dictionary<string, int> Iterations { get; set; } = new Dictionary<string, int>();

        public int Period1 { get; set; }
        public int PeriodA { get; set; }
        public int PeriodB { get; set; }

        public bool Repeat { get; set; }
        public bool RandomTimeInterval { get; set; }
        public int NumberOfRepeats { get; set; }
    }
}
