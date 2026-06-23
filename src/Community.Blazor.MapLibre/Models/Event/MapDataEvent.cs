using System.Text.Json.Serialization;

namespace Community.Blazor.MapLibre.Models.Event;

public class MapDataEvent : MapEvent
{
    [JsonPropertyName("dataType")]
    public string? DataType { get; set; }

    [JsonPropertyName("isSourceLoaded")]
    public bool? IsSourceLoaded { get; set; }

    [JsonPropertyName("sourceId")]
    public string? SourceId { get; set; }

    [JsonPropertyName("sourceDataType")]
    public string? SourceDataType { get; set; }

    [JsonPropertyName("tile")]
    public object? Tile { get; set; }
}
