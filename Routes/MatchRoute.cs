using BasqueteCansadoApi.Data;
using BasqueteCansadoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BasqueteCansadoApi.Routes
{
    public static class MatchRoutes
    {
        public static void MapMatchRoutes(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/matches", async (AppDbContext context) =>
            {

                var matches = await context.Matches.ToListAsync();
                return Results.Ok(matches);

            });

            endpoints.MapGet("/matches/{id}", async (AppDbContext context, Guid id) =>
            {

                var match = await context.Matches.FindAsync(id);
                return match is null ? Results.NotFound() : Results.Ok(match);

            });
            //get all players in a match
            endpoints.MapGet("/matches/{id}/players", async (AppDbContext context, Guid id) =>
            {
                var players = await context.PlayerTeamMatches.Where(playerTeamMatch => playerTeamMatch.MatchId == id).ToListAsync();
                return Results.Ok(players);
            });

            endpoints.MapPost("/matches", async (AppDbContext context) =>
            {

                var NewMatch = new Match();
                NewMatch.Id = Guid.NewGuid();
                Console.WriteLine(NewMatch);
                context.Matches.Add(NewMatch);
                await context.SaveChangesAsync();
                return Results.Created($"/matches/{NewMatch.Id}", NewMatch);

            });

            //endpoints.MapPut("/matches/{id}", async (AppDbContext context, Guid id, Match match) =>
            //{

            //    if (id != match.Id) return Results.BadRequest();
            //    context.Entry(match).State = EntityState.Modified;
            //    await context.SaveChangesAsync();
            //    return Results.Ok(match);

            //});

            endpoints.MapDelete("/matches/{id}", async (AppDbContext context, Guid id) =>
            {

                var match = await context.Matches.FindAsync(id);
                if (match is null) return Results.NotFound();
                context.Matches.Remove(match);
                await context.SaveChangesAsync();
                return Results.NoContent();

            });
        }
    }
}
