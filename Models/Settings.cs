using System.ComponentModel;

namespace Clicker.Models;

public class Settings
{
    public required BindingList<Actions.Action> Moves { get; set; }
    public required List<TagSetting> Tags { get; set; }
    public required Dictionary<string, Queue<int>> Iterations { get; set; }

    public required int Period1 { get; set; }
    public required int PeriodA { get; set; }
    public required int PeriodB { get; set; }

    public required bool Repeat { get; set; }
    public required bool RandomTimeInterval { get; set; }
    public required int NumberOfRepeats { get; set; }
}
