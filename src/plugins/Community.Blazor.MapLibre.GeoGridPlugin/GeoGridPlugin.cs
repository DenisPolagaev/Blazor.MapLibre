using Community.Blazor.MapLibre;
using Microsoft.JSInterop;

namespace Community.Blazor.MapLibre.GeoGridPlugin;

/// <summary>
/// MapLibre plugin that adds a geographic graticule grid
/// (<see href="https://github.com/falseinput/geogrid-maplibre-gl">geogrid-maplibre-gl</see>).
/// </summary>
public sealed class GeoGridPlugin : IMapLibrePlugin
{
    private IJSObjectReference _mapObject = null!;
    private IJSObjectReference _pluginJsModule = null!;

    /// <summary>Whether the grid is currently attached to the map.</summary>
    public bool IsActive { get; private set; }

    public async Task Initialize(IJSObjectReference map, IJSRuntime runtime)
    {
        _mapObject = map;
        _pluginJsModule = await runtime.InvokeAsync<IJSObjectReference>(
            "import", "/_content/GeoGridPlugin/GeoGridPlugin.js");
        await _pluginJsModule.InvokeVoidAsync("initialize", _mapObject);
    }

    /// <summary>
    /// Adds the geographic grid to the map. Replaces any existing grid instance.
    /// </summary>
    public async ValueTask AddGeoGridAsync(GeoGridOptions? options = null)
    {
        await _pluginJsModule.InvokeVoidAsync("add", options);
        IsActive = true;
    }

    /// <summary>
    /// Removes the grid from the map.
    /// </summary>
    public async ValueTask RemoveGeoGridAsync()
    {
        await _pluginJsModule.InvokeVoidAsync("remove");
        IsActive = false;
    }

    /// <summary>
    /// Re-attaches the grid after <see cref="RemoveGeoGridAsync"/> without recreating options.
    /// </summary>
    public async ValueTask ShowGeoGridAsync()
    {
        await _pluginJsModule.InvokeVoidAsync("show");
        IsActive = true;
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            await _pluginJsModule.InvokeVoidAsync("dispose");
            await _pluginJsModule.DisposeAsync();
            IsActive = false;
        }
        catch (JSDisconnectedException) { }
        catch (ObjectDisposedException) { }
    }
}
