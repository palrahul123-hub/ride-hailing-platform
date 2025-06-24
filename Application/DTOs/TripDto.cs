namespace RideHailing.Application.DTOs
{
    public class TripDto
    {
        public Guid Id { get; set; }
        public string PickupLocation { get; set; } = default!;
        public string DropoffLocation { get; set; } = default!;
        public DateTime? ScheduledTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal Fare { get; set; }
        public string Status { get; set; } = default!;
        public string RideType { get; set; } = default!;
        public Guid RiderId { get; set; }
        public Guid? DriverId { get; set; }
    }
}
