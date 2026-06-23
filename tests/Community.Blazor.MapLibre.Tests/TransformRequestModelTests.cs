using System.Text.Json;
using Community.Blazor.MapLibre.Models.Request;
using Xunit;

namespace Community.Blazor.MapLibre.Tests;

public class TransformRequestModelTests
{
    [Fact]
    public void TransformRequestInput_DeserializesFromJson()
    {
        const string json = """
            {
              "url": "https://example.com/tile/1/2/3",
              "resourceType": "Tile"
            }
            """;

        var input = JsonSerializer.Deserialize<TransformRequestInput>(json, MapLibreJsonSerializer.Options);

        Assert.NotNull(input);
        Assert.Equal("https://example.com/tile/1/2/3", input!.Url);
        Assert.Equal("Tile", input.ResourceType);
    }

    [Fact]
    public void TransformRequestResult_SerializesExpectedShape()
    {
        var result = new TransformRequestResult
        {
            Url = "https://example.com/tile/1/2/3",
            Headers = new Dictionary<string, string> { ["Authorization"] = "Bearer token" },
            Method = "GET",
        };

        var json = JsonSerializer.Serialize(result, MapLibreJsonSerializer.Options);
        using var document = JsonDocument.Parse(json);

        Assert.Equal("https://example.com/tile/1/2/3", document.RootElement.GetProperty("url").GetString());
        Assert.Equal("Bearer token", document.RootElement.GetProperty("headers").GetProperty("Authorization").GetString());
        Assert.Equal("GET", document.RootElement.GetProperty("method").GetString());
    }
}
