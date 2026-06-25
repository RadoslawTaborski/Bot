using System.ComponentModel;

namespace Clicker.Models;

public class Settings
{
    public BindingList<Actions.Action> Moves { get; set; } = new BindingList<Actions.Action>();
    public List<TagSetting> Tags { get; set; } = new List<TagSetting>();
    public Dictionary<string, Queue<int>> Iterations { get; set; } = new Dictionary<string, Queue<int>>();

    public int Period1 { get; set; }
    public int PeriodA { get; set; }
    public int PeriodB { get; set; }

    public bool Repeat { get; set; }
    public bool RandomTimeInterval { get; set; }
    public int NumberOfRepeats { get; set; }
}
