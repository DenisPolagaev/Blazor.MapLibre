using System.Text.Json.Serialization;

namespace Community.Blazor.MapLibre.Models.Event;

public class MapWheelEvent : MapMouseEvent
{
    [JsonPropertyName("deltaY")]
    public double? DeltaY { get; set; }

    [JsonPropertyName("deltaX")]
    public double? DeltaX { get; set; }
}
