namespace BasqueteCansadoApi.Dto
{
    public class PlayerTeamMatchDto
    {
        public Guid Id { get; set; }
        public bool IsStarter { get; set; }
        public bool Team { get; set; }
        public Guid PlayerId { get; set; }
        public Guid MatchId { get; set; }
    }
}
