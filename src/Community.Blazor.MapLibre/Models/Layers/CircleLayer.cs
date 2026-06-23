using System.Text.Json.Nodes;
using OneOf;
using System.Text.Json.Serialization;
using Community.Blazor.MapLibre.Converter;

namespace Community.Blazor.MapLibre.Models.Layers;

public class CircleLayer : Layer<CircleLayerLayout, CircleLayerPaint>
{
    /// <summary>
    /// Gets or sets the name of the source to be used for this layer.
    /// </summary>
    [JsonPropertyName("source")]
    public required string Source { get; set; }

    /// <summary>
    /// Gets or sets the layer to use from a vector tile source.
    /// </summary>
    /// <remarks>
    /// Required for vector tile sources. Specifies the layer within the vector tiles to use for this layer.
    /// </remarks>
    [JsonPropertyName("source-layer")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SourceLayer { get; set; }
}

public class CircleLayerLayout
{
    /// <summary>
    /// Sorts features in ascending order based on this value. Features with a higher sort key appear above features with a lower sort key.
    /// </summary>
    [JsonPropertyName("circle-sort-key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? CircleSortKey { get; set; }

    /// <summary>
    /// Controls whether this layer is displayed.
    /// </summary>
    [JsonPropertyName("visibility")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<string>))]
    public OneOf<string, JsonArray>? Visibility { get; set; }
}

public class CircleLayerPaint
{
    [JsonPropertyName("circle-radius")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? CircleRadius { get; set; }

    [JsonPropertyName("circle-color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<string>))]
    public OneOf<string, JsonArray>? CircleColor { get; set; }

    [JsonPropertyName("circle-blur")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? CircleBlur { get; set; }

    [JsonPropertyName("circle-opacity")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? CircleOpacity { get; set; }

    [JsonPropertyName("circle-translate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double[]>))]
    public OneOf<double[], JsonArray>? CircleTranslate { get; set; }

    [JsonPropertyName("circle-translate-anchor")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<MapViewport>))]
    public OneOf<MapViewport, JsonArray>? CircleTranslateAnchor { get; set; }

    [JsonPropertyName("circle-pitch-scale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<MapViewport>))]
    public OneOf<MapViewport, JsonArray>? CirclePitchScale { get; set; }

    [JsonPropertyName("circle-pitch-alignment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<MapViewport>))]
    public OneOf<MapViewport, JsonArray>? CirclePitchAlignment { get; set; }

    [JsonPropertyName("circle-stroke-width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? CircleStrokeWidth { get; set; }

    [JsonPropertyName("circle-stroke-color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<string>))]
    public OneOf<string, JsonArray>? CircleStrokeColor { get; set; }

    [JsonPropertyName("circle-stroke-opacity")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? CircleStrokeOpacity { get; set; }

    /// <summary>
    /// Controls the intensity of light emitted on the source features.
    /// </summary>
    [JsonPropertyName("circle-emissive-strength")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? CircleEmissiveStrength { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MapViewport
{
    [JsonStringEnumMemberName("map")]
    Map,

    [JsonStringEnumMemberName("viewport")]
    Viewport
}
