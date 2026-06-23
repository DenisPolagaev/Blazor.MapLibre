# Background layer

C# type: `BackgroundLayer` — no `source` required.

| Spec property | C# property | Class |
|---------------|-------------|-------|
| `visibility` | `Visibility` | `BackgroundLayerLayout` |
| `background-color` | `BackgroundColor` | `BackgroundLayerPaint` |
| `background-pattern` | `BackgroundPattern` | `BackgroundLayerPaint` |
| `background-opacity` | `BackgroundOpacity` | `BackgroundLayerPaint` |

```csharp
await map.AddLayer(new BackgroundLayer
{
    Id = "background",
    Paint = new BackgroundLayerPaint { BackgroundColor = "#ddeeff" },
});
```
