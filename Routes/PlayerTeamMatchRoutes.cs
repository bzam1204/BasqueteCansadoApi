using BasqueteCansadoApi.Data;
using BasqueteCansadoApi.Dto;
using BasqueteCansadoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BasqueteCansadoApi.Routes
{
    public static class PlayerTeamMatchRoutes
    {

        public static void MapPlayerTeamMatchRoutes(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/matches/{matchId}/add", async (AppDbContext context, Guid playerId, bool team, Guid matchId) =>
            {
                var player = await context.Players.FindAsync(playerId);
                var match = await context.Matches.FindAsync(matchId);

                if (player is null || match is null) return Results.NotFound();

                var newPlayerTeamMatch = new PlayerTeamMatch
                {
                    Id = Guid.NewGuid(),
                    MatchId = matchId,
                    Team = team,
                    Match = match,
                    IsStarter = true,
                    PlayerId = playerId,
                    Player = player
                };
                
                context.PlayerTeamMatches.Add(newPlayerTeamMatch);
                await context.SaveChangesAsync();

                var playerTeamMatchDto = new PlayerTeamMatchDto{
                    Id = newPlayerTeamMatch.Id,
                    IsStarter= newPlayerTeamMatch.IsStarter,
                    Team = newPlayerTeamMatch.Team,
                    PlayerId = newPlayerTeamMatch.PlayerId,
                    MatchId = newPlayerTeamMatch.MatchId
                };

                return Results.Created($"/player-team-match/{playerTeamMatchDto.Id}", playerTeamMatchDto);
            });

            endpoints.MapDelete("/player-team-match/remove", async (AppDbContext context, Guid playerId, Guid matchId) =>
            {
                var playerTeamMatch = await context.PlayerTeamMatches.FirstOrDefaultAsync(ptm => ptm.PlayerId == playerId && ptm.MatchId == matchId);

                if (playerTeamMatch is null) return Results.NotFound();

                context.PlayerTeamMatches.Remove(playerTeamMatch);
                await context.SaveChangesAsync();

                return Results.NoContent();
            });

            endpoints.MapGet("/player-team-match/match{id}", async (AppDbContext context, Guid id) =>
            {
                var playerTeamMatch = await context.PlayerTeamMatches.FindAsync(id);
                return playerTeamMatch is null ? Results.NotFound() : Results.Ok(playerTeamMatch);
            });



        }
    }
}
