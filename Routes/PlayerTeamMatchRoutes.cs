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
            //add player into a team in a match
            endpoints.MapPost("/matches/{matchId}/team/{team}/add-player",
                async (AppDbContext context, Guid playerId, bool team, Guid matchId) =>
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

                    var playerTeamMatchDto = new PlayerTeamMatchDto
                    {
                        Id = newPlayerTeamMatch.Id,
                        IsStarter = newPlayerTeamMatch.IsStarter,
                        Team = newPlayerTeamMatch.Team,
                        PlayerId = newPlayerTeamMatch.PlayerId,
                        MatchId = newPlayerTeamMatch.MatchId
                    };

                    return Results.Created($"/player-team-match/{playerTeamMatchDto.Id}", playerTeamMatchDto);
                });

            //remove player from a team in a match
            endpoints.MapDelete("/match/{matchId}/team/{team}/player/{playerId}/remove",
                async (AppDbContext context, Guid playerId, Guid matchId, bool team, ILogger<Program> logger) =>
                {
                    logger.LogInformation(
                        $"Attempting to remove player. PlayerId: {playerId}, MatchId: {matchId}, Team: {team}");

                    var playerTeamMatch = await context.PlayerTeamMatches
                        .SingleOrDefaultAsync(ptm =>
                            ptm.PlayerId == playerId &&
                            ptm.MatchId == matchId &&
                            ptm.Team == team);

                    if (playerTeamMatch is null)
                    {
                        logger.LogWarning(
                            $"PlayerTeamMatch not found. PlayerId: {playerId}, MatchId: {matchId}, Team: {team}");
                        return Results.NotFound();
                    }

                    logger.LogInformation($"PlayerTeamMatch found. Removing entry.");
                    context.PlayerTeamMatches.Remove(playerTeamMatch);
                    await context.SaveChangesAsync();

                    return Results.Ok($"Player: {playerId} removed from match: {matchId}");
                });

            //get all players from a team in a match
            endpoints.MapGet("/match/{id}/team/{team}/players", async (AppDbContext context, Guid id, bool team) =>
            {
                try
                {
                    // Busca os jogadores que correspondem ao MatchId e Team especificados
                    var players = await context.PlayerTeamMatches
                        .Where(ptm => ptm.MatchId == id && ptm.Team == team)
                        .Select(ptm => new
                        {
                            ptm.PlayerId,
                            // Adicione aqui outros campos do jogador que você deseja retornar
                        })
                        .ToListAsync();

                    // Verifica se a lista de jogadores está vazia
                    if (!players.Any())
                    {
                        return Results.NotFound("Nenhum jogador encontrado para esta partida e time.");
                    }

                    return Results.Ok(players);
                }
                catch (Exception ex)
                {
                    // Log da exceção para diagnóstico
                    Console.WriteLine($"Erro ao buscar jogadores: {ex.Message}");
                    return Results.Problem("Ocorreu um erro ao processar sua solicitação.", statusCode: 500);
                }
            });
        }
    }
}