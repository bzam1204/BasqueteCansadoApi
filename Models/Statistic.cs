namespace BasqueteCansadoApi.Models
{
    public class Statistic
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid MatchId { get; set; }
        public Guid PlayerId { get; set; }
        public bool Team { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public StatisticCategory Category { get; set; }
        public Match Match { get; set; }
        public Player Player { get; set; }
    }
}
