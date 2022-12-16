var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(opt => opt.AddPolicy("AllowAllOrigin", x => x.AllowAnyOrigin()));

var app = builder.Build();
app.UseCors("AllowAllOrigin");

app.MapGet("/", SentEndpoint.HandleRequest);

app.Run();