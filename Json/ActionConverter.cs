using Clicker.Enums;
using Clicker.Models.Actions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Action = Clicker.Models.Actions.Action;

namespace Clicker.Json;

public class ActionConverter : JsonConverter<Action>
{
    public override Action Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        JsonElement root = document.RootElement;

        int type = root.GetProperty("Type").GetInt32();

        return type switch
        {
            (int)Actions.Mouse => JsonSerializer.Deserialize<MouseAction>(root.GetRawText(), options) ?? throw new JsonException("Failed to deserialize MouseAction."),
            (int)Actions.Keyboard => JsonSerializer.Deserialize<KeyboardAction>(root.GetRawText(), options) ?? throw new JsonException("Failed to deserialize KeyboardAction."),
            (int)Actions.SubSequence => JsonSerializer.Deserialize<SubSequenceAction>(root.GetRawText(), options) ?? throw new JsonException("Failed to deserialize SubSequenceAction."),
            (int)Actions.Pause => JsonSerializer.Deserialize<PauseAction>(root.GetRawText(), options) ?? throw new JsonException("Failed to deserialize PauseAction."),
            _ => throw new JsonException("Unknown action type: " + type),
        };
    }

    public override void Write(Utf8JsonWriter writer, Action value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}