namespace RideHailing.Domain.Entities
{
    public class RideType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BaseFare { get; set; }
        public decimal PerKilometerRate { get; set; }
        public decimal PerMinuteRate { get; set; }
        public ICollection<Trip> Trips { get; set; }
    }
}
