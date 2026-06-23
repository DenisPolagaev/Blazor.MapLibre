# Fill layer

C# type: `FillLayer` — requires `source` (and `source-layer` for vector tiles).

Key paint properties: `FillColor`, `FillOpacity`, `FillOutlineColor`, `FillPattern`, `FillLayerOpacity`, `FillEmissiveStrength`.

Layout: `FillSortKey`, `Visibility`.

```csharp
await map.AddLayer(new FillLayer
{
    Id = "regions",
    Source = "geojson",
    Paint = new FillLayerPaint { FillColor = "#4e8752", FillOpacity = 0.8 },
});
```

See [Load GeoJSON example](../../examples/load-geojson.md).
