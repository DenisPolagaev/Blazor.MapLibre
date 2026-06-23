using System.Text.Json;
using Community.Blazor.MapLibre.Models.Event;
using Community.Blazor.MapLibre.Models.LayerFeatures;
using Xunit;

namespace Community.Blazor.MapLibre.Tests;

public class EventModelTests
{
  private const string ClickEventJson = """
    {
      "type": "click",
      "point": { "x": 10, "y": 20 },
      "lngLat": { "lng": 1.5, "lat": 2.5 },
      "features": [
        {
          "type": "Feature",
          "source": "maine",
          "geometry": { "type": "Point", "coordinates": [1, 2] },
          "properties": {},
          "layer": { "id": "GeoJsonLayerId" }
        }
      ]
    }
    """;

    [Fact]
    public void MapMouseEvent_DeserializesFeaturesAsLayerFeatureFeature()
    {
        var evt = JsonSerializer.Deserialize<MapMouseEvent>(ClickEventJson, MapLibreJsonSerializer.Options);

        Assert.NotNull(evt);
        Assert.Equal(EventType.Click, evt!.Type);
        Assert.Single(evt.Features);
        Assert.Equal("maine", evt.Features[0].Source);
        Assert.NotNull(evt.Features[0].Layer);
    }

    [Fact]
    public void MapDataEvent_DeserializesSourceFields()
    {
        const string json = """
            {
              "type": "sourcedata",
              "dataType": "source",
              "isSourceLoaded": true,
              "sourceId": "maine"
            }
            """;

        var evt = JsonSerializer.Deserialize<MapDataEvent>(json, MapLibreJsonSerializer.Options);

        Assert.NotNull(evt);
        Assert.Equal(EventType.SourceData, evt!.Type);
        Assert.Equal("source", evt.DataType);
        Assert.True(evt.IsSourceLoaded);
        Assert.Equal("maine", evt.SourceId);
    }

    [Fact]
    public void MapErrorEvent_DeserializesError()
    {
        const string json = """{ "type": "error", "error": { "message": "test" } }""";

        var evt = JsonSerializer.Deserialize<MapErrorEvent>(json, MapLibreJsonSerializer.Options);

        Assert.NotNull(evt);
        Assert.Equal(EventType.Error, evt!.Type);
        Assert.NotNull(evt.Error);
    }
}
