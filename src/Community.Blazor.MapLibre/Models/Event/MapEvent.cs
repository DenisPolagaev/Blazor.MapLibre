using System.Text.Json;
using System.Text.Json.Serialization;

namespace Community.Blazor.MapLibre.Models.Event;

public class MapEvent
{
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EventType Type { get; set; }

    [JsonPropertyName("originalEvent")]
    public JsonElement? OriginalEvent { get; set; }
}
