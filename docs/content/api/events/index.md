# Events overview

MapLibre GL JS emits many map events. Use `MapEventNames` constants for event names.

| Category | Examples | Scoped to layer? |
|----------|----------|------------------|
| Mouse | `click`, `mousemove`, `mouseenter` | Optional `layerId` |
| Touch | `touchstart`, `touchend` | Optional |
| Camera | `moveend`, `zoom`, `rotateend` | Map only |
| Lifecycle | `load`, `idle`, `error` | Map only |
| Data | `data`, `sourcedata`, `styledata` | Map only |

Typed payloads: `MapMouseEvent`, `MapMoveEvent`, `MapDataEvent`, `MapErrorEvent`.

Component parameters `OnLoad` / `OnStyleLoad` fire once at initialization; use `AddListener` or convenience methods for programmatic subscriptions.

See [listeners](listeners.md) and [map events example](../../examples/map-events.md).
