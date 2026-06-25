using Clicker.Enums;

namespace Clicker.Models.Actions;

public class MouseAction : Action
{
    public MouseActions Button { get; set; }
    public System.Drawing.Point Point { get; set; }

    public override string ToString()
    {
        var tag = string.IsNullOrEmpty(Tag) ? "---" : Tag;
        var description = string.IsNullOrWhiteSpace(Description) ? $"{Point.X}, {Point.Y}" : Description;
        return $"{Id}. {tag} ; {Button} ; {description} ; {Period}";
    }
}
