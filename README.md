# Sent.NET
A .NET clone of the simple plaintext presentation tool, [sent](https://tools.suckless.org/sent/).

## Features
- Each slide's content is a paragraph in a text file
    - Paragraphs starting with @ are interpreted as image URIs (without the @)
    - A paragraph's first character is ignored if it is a backslash
- Slide counter in the bottom right corner
- Navigation with arrow keys

## Components
The server contains a Server-Sent Events endpoint (running on .NET 7) for reading paragraphs from the source file and
a simple HTML + CSS + JS client presenting these events as slides.

## Dependencies
- .NET 7
- A modern browser

## Example usage
Launch the server with `dotnet run ../hello-world.txt --project Server/sent.net.csproj`.

Open the client through its `index.html`.
