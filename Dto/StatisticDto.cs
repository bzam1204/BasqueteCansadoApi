namespace BasqueteCansadoApi.Dto
{
    public class StatisticDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid MatchId { get; set; }
        public Guid PlayerId { get; set; }
        public bool Team { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
