using System.Text.Json;
using System.Text.Json.Serialization;

namespace Clicker.Json;

public class PointConverter : JsonConverter<Point>
{
    public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();

        string[]? parts = value?.Split(',');

        if (parts?.Length == 2)
        {
            int x = int.Parse(parts[0].Trim());
            int y = int.Parse(parts[1].Trim());

            return new Point(x, y);
        }

        throw new JsonException("Failed to deserialize Point");
    }

    public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
    {
        writer.WriteStringValue($"{value.X}, {value.Y}");
    }
}
