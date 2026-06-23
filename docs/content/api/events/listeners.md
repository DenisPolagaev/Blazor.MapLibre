# Event listeners

## Add and remove

```csharp
Listener listener = await map.OnClick("my-layer", e => { ... });
await listener.Remove(); // calls map.off() in JavaScript
```

`Listener` implements `IAsyncDisposable` — use `await using` or `DisposeAsync()` on the component.

## APIs

- `AddListener<T>(eventName, handler, layer?)` — generic subscription
- `AddListener<T>(eventName, handler, params string[] layerIds)` — multiple layers
- `AddOnceListener<T>` / `AddOnceAsyncListener<T>` — single fire (`map.once`)
- `OnClick`, `OnMoveEnd`, `OnIdle`, `OnMouseMove`, `OnData`, `OnSourceData`, `OnError`, `OnTouchStart`, `OnTouchEnd`, `OnZoomChange`, `OnStyleLoadListener`

## Pitfalls

- Always remove listeners when re-subscribing (e.g. before adding a duplicate handler).
- `MapMouseEvent.Features` is `LayerFeatureFeature[]` — use the same shape as `QueryRenderedLayerFeatures`.
- Layer-scoped events require the layer to exist before subscribing.
