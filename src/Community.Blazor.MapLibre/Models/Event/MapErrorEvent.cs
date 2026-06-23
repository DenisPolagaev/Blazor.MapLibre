using System.Text.Json.Serialization;

namespace Community.Blazor.MapLibre.Models.Event;

public class MapErrorEvent : MapEvent
{
    [JsonPropertyName("error")]
    public object? Error { get; set; }
}
