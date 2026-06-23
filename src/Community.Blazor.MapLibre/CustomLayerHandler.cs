using Microsoft.JSInterop;

namespace Community.Blazor.MapLibre;

/// <summary>
/// Base class for Blazor-driven MapLibre custom layers.
/// </summary>
public abstract class CustomLayerHandler
{
    [JSInvokable]
    public Task OnAdd() => OnAddAsync();

    [JSInvokable]
    public Task OnRemove() => OnRemoveAsync();

    [JSInvokable]
    public Task OnRender() => OnRenderAsync();

    protected virtual Task OnAddAsync() => Task.CompletedTask;

    protected virtual Task OnRemoveAsync() => Task.CompletedTask;

    protected virtual Task OnRenderAsync() => Task.CompletedTask;
}
