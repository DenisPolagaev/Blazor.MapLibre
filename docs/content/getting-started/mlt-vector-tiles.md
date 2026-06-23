# MLT vector tiles

MapLibre GL JS 5.12+ supports MapLibre Tiles via `encoding: "mlt"` on a vector source:

```csharp
await map.AddSource("mlt-source", new VectorTileSource
{
    Tiles = ["https://example.com/tiles/{z}/{x}/{y}.mlt"],
    Encoding = "mlt",
});
```

Use a tile endpoint that actually serves MLT-encoded tiles.
