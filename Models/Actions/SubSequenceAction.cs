namespace Clicker.Models.Actions;

public class SubSequenceAction : Action
{
    public required string FileName { get; set; }
    public required int Iterations { get; set; }

    public override string ToString()
    {
        var tag = string.IsNullOrEmpty(Tag) ? "---" : Tag;
        var description = string.IsNullOrWhiteSpace(Description) ? $"{Iterations}x {FileName}" : Description;
        return $"{Id}. {tag} ; Sub ; {description} ; {Period}";
    }
}
