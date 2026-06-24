using System.Text.Json;
using Community.Blazor.MapLibre.FrameratePlugin;
using Community.Blazor.MapLibre.MinimapPlugin;
using Community.Blazor.MapLibre.Models;
using Xunit;

namespace Community.Blazor.MapLibre.Tests;

public sealed class PluginControlOptionsTests
{
    [Fact]
    public void MinimapControlOptions_SerializesExpectedJsonShape()
    {
        var options = new MinimapControlOptions
        {
            Id = "overview",
            Width = "200px",
            Height = "120px",
            Center = new LngLat(-73.99, 40.75),
            Zoom = 8,
            ZoomLevels =
            [
                new MinimapZoomLevel(18, 14, 16),
                new MinimapZoomLevel(10, 6, 8),
            ],
            LineColor = "#08F",
            FillOpacity = 0.25,
            DragPan = false,
        };

        var json = JsonSerializer.Serialize(options);

        Assert.Contains("\"id\":\"overview\"", json);
        Assert.Contains("\"width\":\"200px\"", json);
        Assert.Contains("\"height\":\"120px\"", json);
        Assert.Contains("\"center\":", json);
        Assert.Contains("\"zoomLevels\":", json);
        Assert.Contains("\"lineColor\":\"#08F\"", json);
        Assert.Contains("\"dragPan\":false", json);
    }

    [Fact]
    public void FramerateControlOptions_SerializesExpectedJsonShape()
    {
        var options = new FramerateControlOptions
        {
            Background = "rgba(0,0,0,0.9)",
            Color = "#7cf859",
            Font = "Monaco, Consolas, Courier, monospace",
            GraphHeight = 60,
            GraphWidth = 90,
            Width = 100,
        };

        var json = JsonSerializer.Serialize(options);

        Assert.Contains("\"background\":\"rgba(0,0,0,0.9)\"", json);
        Assert.Contains("\"color\":\"#7cf859\"", json);
        Assert.Contains("\"font\":\"Monaco, Consolas, Courier, monospace\"", json);
        Assert.Contains("\"graphHeight\":60", json);
        Assert.Contains("\"graphWidth\":90", json);
        Assert.Contains("\"width\":100", json);
    }
}
