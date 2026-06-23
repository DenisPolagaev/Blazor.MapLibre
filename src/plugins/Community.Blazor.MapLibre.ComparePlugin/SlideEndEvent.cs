using System.Text.Json.Serialization;

namespace Community.Blazor.MapLibre.ComparePlugin;

public sealed record SlideEndEvent(
    [property: JsonPropertyName("currentPosition")] double CurrentPosition);
