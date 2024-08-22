using BasqueteCansadoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BasqueteCansadoApi.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Player > Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<PlayerTeamMatch> PlayerTeamMatches { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<StatisticCategory> StatisticCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Database=basquete-cansado;Port=5432;User Id=postgres;Password=422171;");
        }
    }
}

