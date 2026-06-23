using System.Text.Json;
using Community.Blazor.MapLibre.Models.Request;
using Microsoft.JSInterop;

namespace Community.Blazor.MapLibre;

internal sealed class TransformRequestCallbackHandler(Func<TransformRequestInput, TransformRequestResult> handler)
{
    private static readonly JsonSerializerOptions Serializer = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    [JSInvokable]
    public string Invoke(string requestJson)
    {
        var input = JsonSerializer.Deserialize<TransformRequestInput>(requestJson, Serializer)
                    ?? new TransformRequestInput();
        var output = handler(input);
        return JsonSerializer.Serialize(output, Serializer);
    }
}
