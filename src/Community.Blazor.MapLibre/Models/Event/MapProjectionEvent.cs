using System.Text.Json.Serialization;

namespace Community.Blazor.MapLibre.Models.Event;

public class MapProjectionEvent : MapEvent
{
    [JsonPropertyName("newProjection")]
    public object? NewProjection { get; set; }
}
