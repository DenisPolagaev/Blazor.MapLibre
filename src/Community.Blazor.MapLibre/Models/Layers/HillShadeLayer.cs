using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Community.Blazor.MapLibre.Converter;
using OneOf;

namespace Community.Blazor.MapLibre.Models.Layers;

public class HillShadeLayer : Layer<HillShadeLayerLayout, HillShadeLayerPaint>
{
    [JsonPropertyName("source")]
    public required string Source { get; set; }
}

public class HillShadeLayerLayout
{
    [JsonPropertyName("visibility")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<string>))]
    public OneOf<string, JsonArray>? Visibility { get; set; }
}

public class HillShadeLayerPaint
{
    [JsonPropertyName("hillshade-illumination-direction")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? HillshadeIlluminationDirection { get; set; }

    [JsonPropertyName("hillshade-illumination-anchor")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<string>))]
    public OneOf<string, JsonArray>? HillshadeIlluminationAnchor { get; set; }

    [JsonPropertyName("hillshade-exaggeration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? HillshadeExaggeration { get; set; }

    [JsonPropertyName("hillshade-shadow-color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<string>))]
    public OneOf<string, JsonArray>? HillshadeShadowColor { get; set; }

    [JsonPropertyName("hillshade-highlight-color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<string>))]
    public OneOf<string, JsonArray>? HillshadeHighlightColor { get; set; }

    [JsonPropertyName("hillshade-accent-color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<string>))]
    public OneOf<string, JsonArray>? HillshadeAccentColor { get; set; }

    [JsonPropertyName("hillshade-method")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<string>))]
    public OneOf<string, JsonArray>? HillshadeMethod { get; set; }
}
