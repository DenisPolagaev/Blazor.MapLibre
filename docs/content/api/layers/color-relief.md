# Color relief layer

C# type: `ColorReliefLayer` — elevation color relief from `RasterDEMTileSource`.

Paint: `ColorReliefColor`, `ColorReliefOpacity`, `Resampling`.

```csharp
await map.AddLayer(new ColorReliefLayer
{
    Id = "relief",
    Source = "dem",
    Paint = new ColorReliefLayerPaint { ColorReliefOpacity = 0.85 },
});
```

See [color relief terrain example](../../examples/color-relief-terrain.md).
