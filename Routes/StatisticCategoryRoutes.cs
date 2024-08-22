using BasqueteCansadoApi.Data;
using BasqueteCansadoApi.Dto;
using BasqueteCansadoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BasqueteCansadoApi.Routes
{
    public static class StatisticCategoryRoutes
    {
        public static void MapStatisticCategoryRoutes(this IEndpointRouteBuilder endpoins)
        {
            //delete a statistic category
            endpoins.MapDelete("/statisticCategories/{id}", async (AppDbContext context, Guid id) =>
            {
                var statisticCategory = await context.StatisticCategories.FindAsync(id);
                if (statisticCategory is null) return Results.NotFound();
                context.StatisticCategories.Remove(statisticCategory);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });
            //get all statistic categories
            endpoins.MapGet("/statisticCategories", async (AppDbContext context) =>
            {
                var statisticCategories = await context.StatisticCategories.ToListAsync();
                return Results.Ok(statisticCategories);
            });
            //get a statistic category by id
            endpoins.MapGet("/statisticCategories/{id}", async (AppDbContext context, Guid id) =>
            {
                var statisticCategory = await context.StatisticCategories.FindAsync(id);
                return statisticCategory is null ? Results.NotFound() : Results.Ok(statisticCategory);
            });
            //create a new statistic category
            endpoins.MapPost("/statisticCategories", async (AppDbContext context, string name) =>
            {
                var newStatisticCategory = new StatisticCategory
                {
                    Id = Guid.NewGuid(),
                    Name = name
                };
                context.StatisticCategories.Add(newStatisticCategory);
                await context.SaveChangesAsync();

                var statisticCategoryDto = new StatisticCategoryDto
                {
                    Id = newStatisticCategory.Id,
                    Name = newStatisticCategory.Name
                };

                return Results.Created($"/statisticCategories/{statisticCategoryDto.Id}", statisticCategoryDto);
            });

        }
    }
}
