namespace Community.Blazor.MapLibre;

/// <summary>
/// Represents a listener that handles the removal of a registered event listener.
/// </summary>
public class Listener(CallbackHandler action) : IAsyncDisposable, IDisposable
{
    public async Task Remove()
    {
        await action.RemoveAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await action.RemoveAsync();
    }

    public void Dispose()
    {
        Remove().GetAwaiter().GetResult();
    }
}
