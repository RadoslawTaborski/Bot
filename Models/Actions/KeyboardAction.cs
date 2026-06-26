namespace Clicker.Models.Actions;

public class KeyboardAction : Action
{
    public required string Text { get; set; }

    public override string ToString()
    {
        var tag = string.IsNullOrEmpty(Tag) ? "---" : Tag;
        var description = string.IsNullOrWhiteSpace(Description) ? $"{Text}" : Description;
        return $"{Id}. {tag} ; Text ; {description} ; {Period}";
    }
}
