using BasqueteCansadoApi.Data;
using BasqueteCansadoApi.Dto;
using BasqueteCansadoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BasqueteCansadoApi.Routes
{
    public static class StatisticRoutes
    {

        public static void MapStatisticRoutes(this IEndpointRouteBuilder endpoints)
        {
            //get all statistics
            endpoints.MapGet("/statistics", async (AppDbContext context) =>
            {
                var statistics = await context.Statistics.ToListAsync();
                return Results.Ok(statistics);
            });
            //get match's statistics
            endpoints.MapGet("/match/{matchId}/statistics", async (AppDbContext context, Guid matchId) =>
            {
                var matchStatistics = await context.Statistics.Where(statictic => statictic.MatchId == matchId).ToListAsync();
                return Results.Ok(matchStatistics);
            });
            // //get player's statistics
            // endpoints.MapGet("players/{playerId}/statistics", async (AppDbContext context, Guid playerId) =>
            // {
            //     var playerStatistics = await context.Statistics.Where(statistic => statistic.PlayerId == playerId).ToListAsync();
            //     return Results.Ok(playerStatistics);
            //
            // });
            //get player's statistics in a match
            endpoints.MapGet("/match/{matchId}/statistics/player/{playerId}", async (AppDbContext context, Guid matchId, Guid playerId) =>
            {
                var playerStatistics = await context.Statistics.Where(statistic => statistic.PlayerId == playerId && statistic.MatchId == matchId).ToListAsync();
                return Results.Ok(playerStatistics);

            });
            //get statistics by categoryId
            endpoints.MapGet("/statistics/category/{categoryId}", async (AppDbContext context, Guid categoryId) =>
            {
                var statistics = await context.Statistics.Where(statistic => statistic.CategoryId == categoryId).ToListAsync();
                return Results.Ok(statistics);

            });
            //get statistics by categoryId in a match
            endpoints.MapGet("/match/{matchId}statistics/category/{categoryId}", async (AppDbContext context, Guid matchId, Guid categoryId) =>
            {
                var statistics = await context.Statistics.Where(statistic => statistic.CategoryId == categoryId && statistic.MatchId == matchId).ToListAsync();
                return Results.Ok(statistics);
            });
            //create a new statistic for a player in a match of a specific category and team
            endpoints.MapPost("/match/{matchId}/statistics/add", async (AppDbContext context, Guid matchId, Guid playerId, Guid categoryId, bool team) =>
            {
                var newStatistic = new Statistic
                {
                    Id = Guid.NewGuid(),
                    CategoryId = categoryId,
                    Team = team,
                    CreatedAt = DateTime.UtcNow,
                    MatchId = matchId,
                    PlayerId = playerId,
                };
                context.Statistics.Add(newStatistic);

                var statisticDto = new StatisticDto
                {
                    Id = newStatistic.Id,
                    CategoryId = newStatistic.CategoryId,
                    MatchId = newStatistic.MatchId,
                    PlayerId = newStatistic.PlayerId,
                    Team = newStatistic.Team,
                    CreatedAt = newStatistic.CreatedAt
                };

                await context.SaveChangesAsync();
                return Results.Created($"/statistics/{statisticDto.Id}", statisticDto);

            });
            //delete the last statistic in a match
            endpoints.MapDelete("/match/{matchId}/statistics/undo", async (AppDbContext context, Guid matchId) =>
            {
                var lastStatistic = await context.Statistics.Where(statistic => statistic.MatchId == matchId).OrderByDescending(statistic => statistic.CreatedAt).FirstOrDefaultAsync();

                if (lastStatistic is null) return Results.NotFound();
                context.Statistics.Remove(lastStatistic);

                await context.SaveChangesAsync();
                return Results.NoContent();
            });
            //delete a statistic by id
            endpoints.MapDelete("/statistics/delete/{id}", async (AppDbContext context, Guid id) =>
            {
                var statistic = await context.Statistics.FindAsync(id);
                if (statistic is null) return Results.NotFound();
                context.Statistics.Remove(statistic);
                await context.SaveChangesAsync();
                return Results.NoContent();

            });
        }

    }
}
