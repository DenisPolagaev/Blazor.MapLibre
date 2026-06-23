using System.Text.Json.Serialization;

namespace Community.Blazor.MapLibre.Models.Request;

/// <summary>
/// Input passed to <see cref="MapLibre.SetTransformRequest"/>.
/// </summary>
public sealed class TransformRequestInput
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("resourceType")]
    public string ResourceType { get; set; } = string.Empty;
}

/// <summary>
/// Result returned from a transform request callback.
/// </summary>
public sealed class TransformRequestResult
{
    [JsonPropertyName("url")]
    public required string Url { get; set; }

    [JsonPropertyName("headers")]
    public Dictionary<string, string>? Headers { get; set; }

    [JsonPropertyName("method")]
    public string? Method { get; set; }

    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("credentials")]
    public string? Credentials { get; set; }

    [JsonPropertyName("collectResourceTiming")]
    public bool? CollectResourceTiming { get; set; }
}
