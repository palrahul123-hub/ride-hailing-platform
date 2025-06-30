namespace RideHailing.Application.Common
{
    public class DriverHub
    {
        public Guid DriverId { get; set; }
        public string ConnectionId { get; set; }
        public DateTime ConnectedAt { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
