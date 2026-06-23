# Map API methods (selected)

The `MapLibre` component wraps MapLibre GL JS 5.17. Key methods added in recent releases:

## Style and terrain

- `SetStyle(style, SetStyleOptions?)` — pass `{ Diff = true }` for incremental style updates (`style.load` fires).
- `SetTerrain(TerrainSpecification?)`, `SetSky`, `SetLight`
- `SetPaintProperty`, `SetLayoutProperty`, `SetGlobalStateProperty`

## GeoJSON

- `SetSourceData`, `UpdateSourceData`, `UpdateSourceDataAsync` (with `waitForCompletion`)

## Camera and time

- `SetTransformConstrain` / `TransformConstrain` parameter on the component
- `TimeControlSetNow`, `TimeControlRestoreNow`, `TimeControlIsFrozen`

## Query

- `QueryRenderedLayerFeatures` — typed `LayerFeatureFeature[]` with `layer` metadata

## Custom layers

- `AddCustomLayer(layerId, CustomLayerOptions, CustomLayerHandler)`

## Container

- `SetContainer(element)` — pass an `HTMLElement` from another window (iframe) before first render (MapLibre 5.17+).

## Default style

`MapOptions.Style` defaults to `MapStyles.OpenStreetMap`.

## Events

- `AddListener<T>`, `AddOnceListener<T>` — generic subscriptions; returns `Listener` with working `Remove()`.
- Convenience: `OnClick`, `OnMoveEnd`, `OnIdle`, `OnMouseMove`, `OnData`, `OnSourceData`, `OnError`, `OnTouchStart`, `OnTouchEnd`, `OnZoomChange`, `OnStyleLoadListener`.
- Use `MapEventNames` for event name constants.

See [Event listeners](../events/listeners.md) and [Layers overview](../layers/index.md).
