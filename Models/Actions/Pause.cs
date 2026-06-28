namespace Clicker.Models.Actions;

public class PauseAction : Action
{
    public override string ToString()
    {
        string tag = string.IsNullOrEmpty(Tag) ? "---" : Tag;
        string description = string.IsNullOrWhiteSpace(Description) ? "---" : Description;
        return $"{Id}. {tag} ; Pause ; {description} ; {Period}";
    }
}
