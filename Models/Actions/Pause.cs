namespace Clicker.Models.Actions;

public class PauseAction : Action
{
    public override string ToString()
    {
        var tag = string.IsNullOrEmpty(Tag) ? "---" : Tag;
        var description = string.IsNullOrWhiteSpace(Description) ? "---" : Description;
        return $"{Id}. {tag} ; Pause ; {description} ; {Period}";
    }
}
