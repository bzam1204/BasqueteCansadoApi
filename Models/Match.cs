namespace BasqueteCansadoApi.Models
{
    public class Match
    {
        public Guid Id { get; set; }
        public DateTime? Start { get; set; } = DateTime.UtcNow;
        public DateTime? End { get; set; }
        public ICollection<Statistic> Statistic { get; set; }
        public ICollection<PlayerTeamMatch> PlayerTeamMatch { get; set; }
    }
}
