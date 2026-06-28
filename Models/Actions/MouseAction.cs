using Clicker.Enums;

namespace Clicker.Models.Actions;

public class MouseAction : Action
{
    public required MouseActions Button { get; set; }
    public required Point Point { get; set; }

    public override string ToString()
    {
        string tag = string.IsNullOrEmpty(Tag) ? "---" : Tag;
        string description = string.IsNullOrWhiteSpace(Description) ? $"{Point.X}, {Point.Y}" : Description;
        return $"{Id}. {tag} ; {Button} ; {description} ; {Period}";
    }
}
