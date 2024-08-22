using BasqueteCansadoApi.Data;
using BasqueteCansadoApi.Routes;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPlayerRoutes();
app.MapMatchRoutes();
app.MapPlayerTeamMatchRoutes();
app.Run();
