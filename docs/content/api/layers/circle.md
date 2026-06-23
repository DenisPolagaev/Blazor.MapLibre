# Circle layer

C# type: `CircleLayer`.

Layout properties belong in `CircleLayerLayout` (`CircleSortKey`, `Visibility`). Paint: `CircleRadius`, `CircleColor`, `CircleStrokeWidth`, `CircleEmissiveStrength`, etc.

```csharp
await map.AddLayer(new CircleLayer
{
    Id = "points",
    Source = "points",
    Paint = new CircleLayerPaint { CircleRadius = 8, CircleColor = "#2980b9" },
});
```

See [circle layer example](../../examples/circle-layer.md).
