using BasqueteCansadoApi.Data;
using BasqueteCansadoApi.Routes;
using System.Net;
using BasqueteCansadoApi.Controllers;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5500", "http://127.0.0.1:5500")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // Use isso se precisar enviar cookies ou autenticação
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddSignalR();
builder.Services.AddSingleton<SocketController>(); // Registrar o controller como singleton


var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();

// Middleware para adicionar o cabe�alho CSP
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "connect-src 'self' ws://localhost:5144");
    await next();
});

// Endpoint para manipular chamadas de serviço
app.MapPost("/api/socket/{id:int}", (int id, SocketController socketController) =>
{
    try
    {
        socketController.HandleServiceCall(id);
        return Results.Ok(new { message = "Chamada de procedimento realizada com sucesso" });
    }
    catch
    {
        return Results.BadRequest(new { error = "Erro ao realizar chamada de procedimento" });
    }
});

// Configurar o hub de notificações
app.MapHub<NotificationHub>("/hubs/notifications");

// app.UseWebSockets();

// app.Map("/ws", async context =>
// {
//     if (!context.WebSockets.IsWebSocketRequest)
//     {
//         context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
//         return;
//     }
//
//     var socketRoutes = context.RequestServices.GetRequiredService<SocketRoutes>();
//     using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
//     await socketRoutes.HandleWebSocketAsync(webSocket);
// });


app.MapPlayerRoutes();
app.MapMatchRoutes();
app.MapPlayerTeamMatchRoutes();
app.MapStatisticRoutes();
app.MapStatisticCategoryRoutes();

await app.RunAsync();