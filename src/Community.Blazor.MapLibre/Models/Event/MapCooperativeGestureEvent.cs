using System.Text.Json.Serialization;

namespace Community.Blazor.MapLibre.Models.Event;

public class MapCooperativeGestureEvent : MapEvent
{
    [JsonPropertyName("gestureType")]
    public string? GestureType { get; set; }
}
