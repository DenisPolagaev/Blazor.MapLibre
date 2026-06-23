namespace Community.Blazor.MapLibre.Examples;

public static class ExampleMapStyles
{
  public static object OpenStreetMap { get; } = new
  {
    version = 8,
    sources = new Dictionary<string, object>
    {
      ["osm"] = new
      {
        type = "raster",
        tiles = new[] { "https://tile.openstreetmap.org/{z}/{x}/{y}.png" },
        tileSize = 256,
        attribution = "© OpenStreetMap contributors",
        maxzoom = 19,
      },
    },
    layers = new[]
    {
      new
      {
        id = "osm",
        type = "raster",
        source = "osm",
      },
    },
  };
}
