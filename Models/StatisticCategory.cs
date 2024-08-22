namespace BasqueteCansadoApi.Models
{
    public class StatisticCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Statistic> Statistics { get; set; }
    }
}
