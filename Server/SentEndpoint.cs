public static class SentEndpoint
{
    public static async Task HandleRequest(HttpContext context)
    {
        int count = 0;
        var lastLine = string.Empty;

        var sse = await SseResponder.CreateAsync(context.Response);

        var filePath = Environment.GetCommandLineArgs()[1];
        var lines = FileReader.LinesAsyncEnumerator(filePath);

        await foreach (var line in lines)
        {
            if (lastLine == string.Empty)
            {
                await sse.SendIdAsync(count++);
            }

            lastLine = line;
            await sse.SendDataAsync(line);

            if (line == string.Empty)
            {
                await sse.FlushAsync();
            }
        }

        await sse.SendDataAsync();
        await sse.SendEventAsync("close");
        await sse.SendDataAsync("close\n");
        await sse.FlushAsync();
    }
}