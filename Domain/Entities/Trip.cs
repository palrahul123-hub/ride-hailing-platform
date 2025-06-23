using RideHailing.Domain.Enums;

namespace RideHailing.Domain.Entities
{
    public class Trip
    {
        public Guid Id { get; set; }
        public Guid RiderId { get; set; }
        public Guid? DriverId { get; set; }
        public string PickUpLocation { get; set; }
        public string DropOffLocation { get; set; }
        public DateTime? ScheduleTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public decimal? Fare { get; set; }
        public TripStatus Status { get; set; }
        public int RideTypeId { get; set; }
        public User Rider { get; set; }
        public User Driver { get; set; }

        public RideType RideType { get; set; }


    }
}
