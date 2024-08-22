namespace BasqueteCansadoApi.Models
{
    public class PlayerTeamMatch
    {
        public Guid Id { get; set; }
        public bool IsStarter { get; set; } = true;
        public bool Team { get; set; }
        public Guid PlayerId { get; set; }
        public Guid MatchId { get; set; }
        public Player? Player { get; set; }
        public Match? Match { get; set; }
    }
}
