using BasqueteCansadoApi.Data;
using BasqueteCansadoApi.Routes;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseWebSockets();

app.Map("/ws", async context =>
{
    //if its not a websocket req we return a bad request
    if (!context.WebSockets.IsWebSocketRequest)
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    //else, we accept it
    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();


    while (true)
    {
    var data = Encoding.UTF8.GetBytes($"hello world! It's {DateTime.Now}");
        await webSocket.SendAsync(
            data, //message
            WebSocketMessageType.Text, //type
            true, // end of message?
            CancellationToken.None);
        await Task.Delay(1000);
    }
});


app.MapPlayerRoutes();
app.MapMatchRoutes();
app.MapPlayerTeamMatchRoutes();
app.MapStatisticRoutes();
app.MapStatisticCategoryRoutes();

await app.RunAsync();
