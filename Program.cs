using BasqueteCansadoApi.Data;
using BasqueteCansadoApi.Routes;
using System.Net;
using BasqueteCansadoApi.Controllers;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5500", "http://127.0.0.1:5500", "http://localhost:3000", "http://localhost:3001")
        .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); 


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

app.MapPlayerRoutes();
app.MapMatchRoutes();
app.MapPlayerTeamMatchRoutes();
app.MapStatisticRoutes();
app.MapStatisticCategoryRoutes();

await app.RunAsync();