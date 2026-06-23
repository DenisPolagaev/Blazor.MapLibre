using System.Collections.Concurrent;
using Community.Blazor.MapLibre;
using Microsoft.JSInterop;

namespace Community.Blazor.MapLibre.TerraDrawPlugin;

public sealed class TerraDrawPlugin : IMapLibrePlugin
{
    private IJSObjectReference _mapObject = null!;
    private IJSObjectReference _pluginJsModule = null!;
    private readonly ConcurrentDictionary<Guid, DotNetObjectReference<CallbackHandler>> _references = new();

    public async Task Initialize(IJSObjectReference map, IJSRuntime runtime)
    {
        _mapObject = map;
        _pluginJsModule = await runtime.InvokeAsync<IJSObjectReference>(
            "import", "/_content/TerraDrawPlugin/TerraDrawPlugin.js");
        await _pluginJsModule.InvokeVoidAsync("initialize", _mapObject);
    }

    /// <summary>
    /// Add a terra-draw instance for drawing geometries.
    /// </summary>
    /// <param name="singleFeature">
    /// When <c>true</c>, clicking the first/last coordinate no longer closes
    /// the geometry — commit explicitly via <see cref="FinishGeometryAsync"/>.
    /// </param>
    public async Task AddTerraDrawToolAsync(bool singleFeature = false)
    {
        await _pluginJsModule.InvokeVoidAsync("addTerraDrawTool", new { singleFeature });
    }

    /// <summary>
    /// Stop terra-draw and clear its edit state.
    /// </summary>
    public async Task StopTerraDrawAsync()
    {
        await _pluginJsModule.InvokeVoidAsync("stopTerraDraw");
    }

    /// <summary>
    /// Start the selected terra-draw mode.
    /// </summary>
    public async Task SetTerraDrawModeAsync(string mode)
    {
        await _pluginJsModule.InvokeVoidAsync("setTerraDrawMode", mode);
    }

    /// <summary>
    /// Seed terra-draw with an existing GeoJSON feature and switch into select mode.
    /// </summary>
    public async Task EditTerraDrawFeatureAsync(string featureJson, string mode)
    {
        await _pluginJsModule.InvokeVoidAsync("editTerraDrawFeature", featureJson, mode);
    }

    /// <summary>
    /// Finish / close the geometry being edited (simulates enter-key event).
    /// </summary>
    public async Task FinishGeometryAsync()
    {
        await _pluginJsModule.InvokeVoidAsync("finishGeometry");
    }

    /// <summary>
    /// Get created geometries from terra-draw.
    /// </summary>
    public async ValueTask<object[]> GetTerraDrawGeometriesAsync()
    {
        return await _pluginJsModule.InvokeAsync<object[]>("getTerraDrawGeometries");
    }

    public async Task<Listener> AddTerraDrawFinishListener<T>(Action<T> handler)
    {
        var callback = new CallbackHandler(_pluginJsModule, "finish", handler, typeof(T));
        var reference = DotNetObjectReference.Create(callback);
        _references.TryAdd(Guid.NewGuid(), reference);

        await _pluginJsModule.InvokeVoidAsync("onTerraDrawFinish", reference);

        return new Listener(callback);
    }

    public async Task<Listener> AddTerraDrawDeleteListener<T>(Action<T> handler)
    {
        var callback = new CallbackHandler(_pluginJsModule, "delete", handler, typeof(T));
        var reference = DotNetObjectReference.Create(callback);
        _references.TryAdd(Guid.NewGuid(), reference);

        await _pluginJsModule.InvokeVoidAsync("onTerraDrawDelete", reference);

        return new Listener(callback);
    }

    public async Task<Listener> AddTerraDrawChangeListener<T>(Action<T> handler, int throttleTime)
    {
        var callback = new CallbackHandler(_pluginJsModule, "change", handler, typeof(T));
        var reference = DotNetObjectReference.Create(callback);
        _references.TryAdd(Guid.NewGuid(), reference);

        await _pluginJsModule.InvokeVoidAsync("onTerraDrawChange", reference, throttleTime);

        return new Listener(callback);
    }

    /// <summary>
    /// Disables zoom gestures that conflict with terra-draw point editing.
    /// </summary>
    public async ValueTask DisableMapZoomGesturesAsync()
    {
        await _pluginJsModule.InvokeVoidAsync("disableMapZoomGestures");
    }

    /// <summary>
    /// Re-enables zoom gestures disabled by <see cref="DisableMapZoomGesturesAsync"/>.
    /// </summary>
    public async ValueTask EnableMapZoomGesturesAsync()
    {
        await _pluginJsModule.InvokeVoidAsync("enableMapZoomGestures");
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var reference in _references.Values)
        {
            reference.Dispose();
        }

        _references.Clear();

        try
        {
            if (_pluginJsModule is not null)
            {
                await _pluginJsModule.InvokeVoidAsync("dispose");
                await _pluginJsModule.DisposeAsync();
            }
        }
        catch (JSDisconnectedException) { }
        catch (ObjectDisposedException) { }
    }
}
