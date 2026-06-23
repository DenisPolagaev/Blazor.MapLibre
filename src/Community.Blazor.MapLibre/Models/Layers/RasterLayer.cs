using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Community.Blazor.MapLibre.Converter;
using OneOf;

namespace Community.Blazor.MapLibre.Models.Layers;

public class RasterLayer : Layer<RasterLayerLayout, RasterLayerPaint>
{
    [JsonPropertyName("source")]
    public required string Source { get; set; }
}

public class RasterLayerLayout
{
    [JsonPropertyName("visibility")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<string>))]
    public OneOf<string, JsonArray>? Visibility { get; set; }
}

public class RasterLayerPaint
{
    [JsonPropertyName("raster-opacity")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? RasterOpacity { get; set; }

    [JsonPropertyName("raster-hue-rotate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? RasterHueRotate { get; set; }

    [JsonPropertyName("raster-brightness-min")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? RasterBrightnessMin { get; set; }

    [JsonPropertyName("raster-brightness-max")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? RasterBrightnessMax { get; set; }

    [JsonPropertyName("raster-saturation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? RasterSaturation { get; set; }

    [JsonPropertyName("raster-contrast")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? RasterContrast { get; set; }

    [JsonPropertyName("raster-fade-duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<double>))]
    public OneOf<double, JsonArray>? RasterFadeDuration { get; set; }

    [JsonPropertyName("raster-resampling")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OneOfJsonConverter<string>))]
    public OneOf<string, JsonArray>? RasterResampling { get; set; }
}
