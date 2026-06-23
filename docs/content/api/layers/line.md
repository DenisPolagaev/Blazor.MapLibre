# Line layer

C# type: `LineLayer`.

Key paint: `LineColor`, `LineWidth`, `LineOpacity`, `LineDasharray`, `LineGradient`, `LineLayerOpacity`.

```csharp
await map.AddLayer(new LineLayer
{
    Id = "route",
    Source = "route",
    Paint = new LineLayerPaint { LineColor = "#e74c3c", LineWidth = 4 },
});
```

See [line layer example](../../examples/line-layer.md).
