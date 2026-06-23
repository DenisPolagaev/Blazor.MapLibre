# Symbol layer

C# type: `SymbolLayer` — the largest layer model (icon and text layout/paint).

Common layout: `IconImage`, `TextField`, `TextFont`, `TextSize`, `TextAnchor`.

```csharp
await map.AddLayer(new SymbolLayer
{
    Id = "labels",
    Source = "cities",
    Layout = new SymbolLayerLayout
    {
        TextField = new JsonArray { "get", "name" },
        TextSize = 14,
    },
});
```

See [symbol labels example](../../examples/symbol-labels.md).
