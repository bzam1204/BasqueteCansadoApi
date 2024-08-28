using BasqueteCansadoApi.Data;
using BasqueteCansadoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BasqueteCansadoApi.Routes
{
    public static class PlayerRoutes
    {
        public static void MapPlayerRoutes(this IEndpointRouteBuilder endpoints)
        {
            //get all players
            endpoints.MapGet("/players", async (AppDbContext context) =>
            {
                var players = await context.Players.ToListAsync();
                return Results.Ok(players);
            });

            //get player by id
            endpoints.MapGet("/players/{id}", async (AppDbContext context, Guid id) =>
            {
                var player = await context.Players.FindAsync(id);
                return player is null ? Results.NotFound() : Results.Ok(player);
            });

            //create new player
            endpoints.MapPost("/players", async (AppDbContext context, string Name, int? ShirtNumber, string? ImageUrl) =>
            {
                var NewPlayer = new Player();
                NewPlayer.Id = Guid.NewGuid();
                NewPlayer.Name = Name;
                NewPlayer.ShirtNumber = ShirtNumber ?? 00;
                NewPlayer.ImageUrl = ImageUrl ?? null;
                Console.WriteLine(NewPlayer);
                context.Players.Add(NewPlayer);
                await context.SaveChangesAsync();
                return Results.Created($"/players/{NewPlayer.Id}", NewPlayer);
            });

            //update player
            endpoints.MapPut("/players/{id}", async (AppDbContext context, Guid id, string? Name, int? ShirtNumber, string? ImageUrl) =>
            {
                var player = await context.Players.FindAsync(id);
                if (player is null) return Results.NotFound();
                player.Name = Name;
                player.ShirtNumber = ShirtNumber ?? player.ShirtNumber;
                player.ImageUrl = ImageUrl ?? player.ImageUrl;
                context.Entry(player).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Results.Ok(player);
            });

            //delete player
            endpoints.MapDelete("/players/{id}", async (AppDbContext context, Guid id) =>
            {
                try
                {
                    var player = await context.Players.FindAsync(id);
                    if (player is null) return Results.NotFound();
                    context.Players.Remove(player);
                    await context.SaveChangesAsync();
                    return Results.Ok($"player {player.Name} | {player.Id} deleted successfully");
                }
                catch (Exception ex)
                {
                    // Log the exception (ex) here if needed
                    return Results.Problem("An error occurred while deleting the player.");
                }
            });

            //get player statistics
            endpoints.MapGet("/players/{id}/statistics", async (AppDbContext context, Guid id) =>
            {
                try
                {
                    var player = await context.Players.Include(p => p.Statistics).FirstOrDefaultAsync(p => p.Id == id);
                    return player is null ? Results.NotFound("Player not found.") : Results.Ok(player.Statistics);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });

            //get player matches
            endpoints.MapGet("/players/{id}/matches", async (AppDbContext context, Guid id) =>
            {
                var player = await context.Players.Include(p => p.PlayerTeamMatch).ThenInclude(ptm => ptm.Match).FirstOrDefaultAsync(p => p.Id == id);
                return player is null ? Results.NotFound() : Results.Ok(player.PlayerTeamMatch.Select(ptm => ptm.Match));
            });

        }
    }
}
