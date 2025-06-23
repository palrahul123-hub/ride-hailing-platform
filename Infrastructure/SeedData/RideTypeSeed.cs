using Microsoft.EntityFrameworkCore;
using RideHailing.Domain.Entities;

namespace RideHailing.Infrastructure.SeedData
{
    public static class RideTypeSeed
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<RideType>().HasData(new RideType()
            {
                Id = 1,
                Name = "Standard",
                BaseFare = 5.00m,
                PerKilometerRate = 1.50m,
                PerMinuteRate = 0.50m
            },
            new RideType
            {
                Id = 2,
                Name = "Luxury",
                BaseFare = 10.00m,
                PerKilometerRate = 3.00m,
                PerMinuteRate = 1.00m
            }, new RideType
            {
                Id = 3,
                Name = "Carpool",
                BaseFare = 3.00m,
                PerKilometerRate = 1.00m,
                PerMinuteRate = 0.30m
            });
        }
    }
}
