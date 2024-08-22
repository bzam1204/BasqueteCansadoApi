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
        public object Teams { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=basquetecansado-pg.postgres.database.azure.com;Database=postgres;Port=5432;User Id=postgres;Password=1204B,gl;Ssl Mode=Require;");
        }
    }
}
