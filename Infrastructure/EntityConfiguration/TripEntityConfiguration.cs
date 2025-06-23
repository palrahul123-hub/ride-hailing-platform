using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideHailing.Domain.Entities;

namespace RideHailing.Infrastructure.EntityConfiguration
{
    public class TripEntityConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(t => t.PickUpLocation).IsRequired().HasMaxLength(255);
            builder.Property(t => t.DropOffLocation).IsRequired().HasMaxLength(255);



            builder.HasOne(x => x.Rider).WithMany(x => x.Trips).HasForeignKey(x => x.RiderId).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            builder.HasOne(x => x.Driver).WithMany(x => x.Trips).HasForeignKey(x => x.DriverId).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
        }
    }
}
