using System.Text.Json;

namespace Community.Blazor.MapLibre;

/// <summary>
/// Shared JSON serializer options for MapLibre interop and event deserialization.
/// </summary>
public static class MapLibreJsonSerializer
{
    public static JsonSerializerOptions Options { get; } = new()
    {
        AllowOutOfOrderMetadataProperties = true,
        PropertyNameCaseInsensitive = true,
    };
}
