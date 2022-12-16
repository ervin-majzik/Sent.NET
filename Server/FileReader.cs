public static class FileReader
{
    public static async IAsyncEnumerable<string?> LinesAsyncEnumerator(string filePath)
    {
        using var streamReader = new StreamReader(filePath);
        
        while (!streamReader.EndOfStream)
        {
            yield return await streamReader.ReadLineAsync();
        }
    }
}