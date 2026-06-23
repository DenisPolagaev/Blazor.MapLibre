# Fill extrusion layer

C# type: `FillExtrusionLayer` — 3D extruded polygons.

Paint: `FillExtrusionHeight`, `FillExtrusionBase`, `FillExtrusionColor`, `FillExtrusionOpacity`, `FillExtrusionPattern`.

```csharp
Paint = new FillExtrusionLayerPaint
{
    FillExtrusionHeight = new JsonArray { "get", "height" },
    FillExtrusionColor = "#aaa",
}
```

See [fill extrusion example](../../examples/fill-extrusion.md).
