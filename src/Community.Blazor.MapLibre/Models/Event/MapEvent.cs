using System.Text.Json.Serialization;

namespace Community.Blazor.MapLibre.Models.Event;

public class MapEvent
{
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required EventType Type { get; set; }
}