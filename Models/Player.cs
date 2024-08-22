namespace BasqueteCansadoApi.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int ShirtNumber { get; set; }
        public ICollection<Statistic> Statistics { get; set; }
        public ICollection<PlayerTeamMatch> PlayerTeamMatch { get; set; }
    }
}