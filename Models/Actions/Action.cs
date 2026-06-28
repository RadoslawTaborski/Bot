using System.Text.Json.Serialization;

namespace Clicker.Models.Actions;

public abstract class Action
{
    public required string Id { get; set; }
    public required int Period { get; set; }
    public string? Tag { get; set; }
    public string? Description { get; set; } = string.Empty;
    public required bool Active { get; set; } = true;
    public required Enums.Actions Type { get; set; }

    public Action Clone()
    {
        Action clone = (Action)MemberwiseClone();
        clone.Guid = Guid.NewGuid();
        return clone;
    }

    [JsonIgnore]
    public Guid Guid { get; set; } = Guid.NewGuid();
}
