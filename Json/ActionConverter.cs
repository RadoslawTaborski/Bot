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
        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;

        var type = root.GetProperty("Type").GetInt32();

        if (type == (int)Actions.Mouse)
        {
            return JsonSerializer.Deserialize<MouseAction>(
                root.GetRawText(),
                options);
        }
        else if (type == (int)Actions.Keyboard)
        {
            return JsonSerializer.Deserialize<KeyboardAction>(
                root.GetRawText(),
                options);
        }
        else if (type == (int)Actions.SubSequence)
        {
            return JsonSerializer.Deserialize<SubSequenceAction>(
                root.GetRawText(),
                options);
        }
        else if (type == (int)Actions.Pause)
        {
            return JsonSerializer.Deserialize<PauseAction>(
                root.GetRawText(),
                options);
        }

        throw new JsonException("Unknown action type: " + type);

    }

    public override void Write(Utf8JsonWriter writer, Action value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}