using System.Runtime.CompilerServices;

public class SseResponder
{
    private readonly StreamWriter _streamWriter;

    private SseResponder(StreamWriter streamWriter)
    {
        _streamWriter = streamWriter;
    }

    public static async Task<SseResponder> CreateAsync(HttpResponse response)
    {
        response.ContentType = "text/event-stream";
        await response.Body.FlushAsync();
        return new SseResponder(new StreamWriter(response.Body));
    }

    public async Task FlushAsync() => await _streamWriter.FlushAsync();
    public async Task SendIdAsync(int id) => await Send(id);
    public async Task SendDataAsync(string? data = "") => await Send(data);
    public async Task SendEventAsync(string? @event) => await Send(@event, "event");

    private async Task Send(object? data, [CallerArgumentExpression("data")] string type = "")
    {
        var message = data?.ToString();

        if (!string.IsNullOrWhiteSpace(message))
        {
            message = $"{type}: {message}";
        }
        
        await _streamWriter.WriteLineAsync(message);
    }
}