# Terra Draw plugin

The **Terra Draw plugin** adds interactive drawing and geometry editing to a `MapLibre` map. It wraps [Terra Draw](https://github.com/JamesLMilner/terra-draw) and the [MapLibre GL adapter](https://github.com/JamesLMilner/terra-draw/tree/main/packages/terra-draw-maplibre-gl-adapter) as an optional plugin, keeping the core library free of drawing dependencies.

The plugin project lives at `src/plugins/Community.Blazor.MapLibre.TerraDrawPlugin`.

## Installation

Add a project reference to the Terra Draw plugin Razor Class Library:

```shell
dotnet add reference ../../src/plugins/Community.Blazor.MapLibre.TerraDrawPlugin/Community.Blazor.MapLibre.TerraDrawPlugin.csproj
```

Or reference the plugin project from your solution in Visual Studio / Rider.

## Register the plugin

Register the plugin on the `MapLibre` component in `OnAfterRenderAsync` with `firstRender` so the `@ref` to the map is available:

```csharp
@using Community.Blazor.MapLibre.TerraDrawPlugin

<MapLibre @ref="_map" Options="_options" OnLoad="OnMapLoad" />

@code {
    private MapLibre _map = new();
    private TerraDrawPlugin _terraDraw = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await _map.RegisterPlugin(_terraDraw);
        }
    }

    private async Task OnMapLoad(EventArgs _)
    {
        await _terraDraw.AddTerraDrawToolAsync();
        await _terraDraw.SetTerraDrawModeAsync("polygon");
    }
}
```

The plugin loads its JavaScript dependencies automatically during `Initialize`. No extra `<script>` tags are required in your host application.

## Drawing modes

After calling `AddTerraDrawToolAsync`, switch modes with `SetTerraDrawModeAsync`:

| Mode | Description |
| --- | --- |
| `point` | Draw point features |
| `linestring` | Draw line features |
| `polygon` | Draw polygon features |
| `freehand` | Freehand drawing |
| `select` | Select and drag coordinates |
| `delete` | Delete coordinates (custom delete mode) |
| `static` | Stop interaction (used internally by `StopTerraDrawAsync`) |

## API overview

| Method | Description |
| --- | --- |
| `AddTerraDrawToolAsync(singleFeature)` | Initialise Terra Draw on the map. When `singleFeature` is `true`, geometry is not auto-closed on the last click; call `FinishGeometryAsync` to commit. |
| `StopTerraDrawAsync()` | Stop drawing and clear the active edit session. |
| `SetTerraDrawModeAsync(mode)` | Activate a drawing or editing mode. |
| `EditTerraDrawFeatureAsync(featureJson, mode)` | Load an existing GeoJSON feature and enter select mode for editing. |
| `FinishGeometryAsync()` | Finish the current geometry (simulates pressing Enter). |
| `GetTerraDrawGeometriesAsync()` | Return the current user-drawn features (helper points filtered out). |
| `DisableMapZoomGesturesAsync()` | Disable double-click / tap-drag zoom while editing vertices. |
| `EnableMapZoomGesturesAsync()` | Re-enable zoom gestures disabled during editing. |

## Listening for changes

Use `Listener`-based helpers to react to draw events. Handlers receive a JSON string of the current feature snapshot:

```csharp
private Listener? _changeListener;

private async Task OnMapLoad(EventArgs _)
{
    await _terraDraw.AddTerraDrawToolAsync();
    await _terraDraw.SetTerraDrawModeAsync("polygon");

    _changeListener = await _terraDraw.AddTerraDrawChangeListener<string>(
        geoJson => { _snapshot = geoJson; StateHasChanged(); },
        throttleTime: 200);
}
```

| Listener method | Fires when |
| --- | --- |
| `AddTerraDrawFinishListener<T>` | A draw or coordinate drag finishes |
| `AddTerraDrawDeleteListener<T>` | Features are deleted |
| `AddTerraDrawChangeListener<T>` | Features are created, updated, or deleted (throttled) |

## Editing an existing feature

Pass a GeoJSON Feature as a JSON string and the Terra Draw mode that matches its geometry:

```csharp
await _terraDraw.AddTerraDrawToolAsync();
await _terraDraw.EditTerraDrawFeatureAsync(
    """{"type":"Feature","geometry":{"type":"Polygon","coordinates":[[[0,0],[0,1],[1,1],[1,0],[0,0]]]},"properties":{}}""",
    "polygon");
```

While an edit session is active, `GetTerraDrawGeometriesAsync` returns only the seeded feature. The session ends when the user switches to a non-select mode or when `StopTerraDrawAsync` is called.

## Related topics

- [Create a plugin](./create-a-plugin.md) — build your own plugin from scratch
- [Key concepts](../getting-started/key-concepts.md#plugin-system) — plugin architecture overview
- [Terra Draw example](./examples/terra-draw.md) — live demo
