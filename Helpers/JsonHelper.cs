using Clicker.Json;
using Clicker.Models;
using System.Runtime.Serialization;
using System.Text.Json;

namespace Clicker.Helpers;

public class JsonHelper
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        Converters = { new ActionConverter(), new Json.PointConverter() }
    };

    public Settings ReadSettings(string path)
    {
        string json = File.ReadAllText(path);
        Settings? settings = JsonSerializer.Deserialize<Settings>(json, _jsonOptions);
        return settings is null ? throw new SerializationException("Deserialized Settings is null.") : settings;

    }

    public string SerializeSettings(Settings settings)
    {
        return JsonSerializer.Serialize(settings, _jsonOptions);
    }
}