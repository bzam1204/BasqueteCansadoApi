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
                    var playerTeamMatch = await context.PlayerTeamMatches
                        .SingleOrDefaultAsync(ptm =>
                            ptm.PlayerId == playerId &&
                            ptm.MatchId == matchId &&
                            ptm.Team == team);

                    if (playerTeamMatch != null) return Results.NotFound();
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
                            ptm.Player
                            // Adicione aqui outros campos do jogador que você deseja retornar
                        })
                        .ToListAsync();

                    return Results.Ok(players);
                }
                catch (Exception ex)
                {
                    // Log da exceção para diagnóstico
                    Console.WriteLine($"Erro ao buscar jogadores: {ex.Message}");
                    return Results.Problem("Ocorreu um erro ao processar sua solicitação.", statusCode: 500);
                }
            });

            //substitute player in a match
            endpoints.MapPut("match/{matchId}/substitute",
                async (AppDbContext context, Guid matchId, Guid? playerOneId, Guid? playerTwoId) =>
                {
                    if (playerOneId is null && playerTwoId is null) return Results.BadRequest("Jogadores não informados");
                    try
                    {
                        var ptms = await context.PlayerTeamMatches
                            .Where(ptm =>
                                ptm.MatchId == matchId && ptm.PlayerId == playerOneId || ptm.PlayerId == playerTwoId)
                            .ToArrayAsync();


                        if (ptms.Length == 0) return Results.NotFound();
                        if (ptms.Length == 2)
                        {
                            var playerOne = ptms[0];
                            var playerTwo = ptms[1];

                            playerOne.Team = !playerOne.Team;
                            playerTwo.Team = !playerTwo.Team;

                            context.PlayerTeamMatches.UpdateRange(playerOne, playerTwo);
                            await context.SaveChangesAsync();
                        }

                        if (ptms.Length == 1)
                        {
                            var playerOnGame = ptms[0];
                            Player playerOffGame;

                            if (playerOnGame.PlayerId == playerOneId)
                                playerOffGame = await context.Players.FindAsync(playerTwoId);
                            else
                                playerOffGame = await context.Players.FindAsync(playerOneId);

                            playerOnGame.IsStarter = false;

                            var newPlayerTeamMatch = new PlayerTeamMatch
                            {
                                Id = Guid.NewGuid(),
                                MatchId = matchId,
                                Team = playerOnGame.Team,
                                Match = playerOnGame.Match,
                                IsStarter = true,
                                PlayerId = playerOffGame.Id,
                                Player = playerOffGame
                            };

                            context.PlayerTeamMatches.Add(newPlayerTeamMatch);
                            context.PlayerTeamMatches.Update(playerOnGame);
                            await context.SaveChangesAsync();
                        }

                        return Results.Ok("Substituição realizada com sucesso");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                });
        }
    }
}