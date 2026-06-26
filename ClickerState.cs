using Clicker.Models;
using Action = Clicker.Models.Actions.Action;

namespace Clicker;

public class ClickerState
{
    public Settings Settings { get; set; }
    public List<Action> CurrentSequence { get; set; }
    public int CurrentIndex { get; set; }
    public int IterationsCounter { get; set; }
    public Dictionary<string, Queue<int>> OverrideIterationsQueues { get; set; }
    public Dictionary<Guid, string> NestedSequencesNotes { get; set; }

    public ClickerState()
    {
        Settings = new Settings
        {
            Moves = [],
            Tags = [],
            Iterations = [],
            Period1 = 2000,
            PeriodA = 2000,
            PeriodB = 2000,
            Repeat = false,
            RandomTimeInterval = false,
            NumberOfRepeats = 2
        };
        CurrentIndex = 1;
        IterationsCounter = 0;
        OverrideIterationsQueues = [];
        NestedSequencesNotes = [];
        CurrentSequence = [];
    }

    public void Reset()
    {
        CurrentIndex = 1;
        IterationsCounter = 0;
        OverrideIterationsQueues.Clear();
        NestedSequencesNotes.Clear();
        CurrentSequence.Clear();
        Settings.Moves.Clear();
        Settings.Tags.Clear();
        Settings.Iterations.Clear();
        Settings.Period1 = 2000;
        Settings.PeriodA = 2000;
        Settings.PeriodB = 2000;
        Settings.Repeat = false;
        Settings.RandomTimeInterval = false;
        Settings.NumberOfRepeats = 1;
    }
}
