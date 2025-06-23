using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideHailing.Domain.Entities;

namespace RideHailing.Infrastructure.EntityConfiguration
{
    public class RideTypeEntityConfiguration : IEntityTypeConfiguration<RideType>
    {
        public void Configure(EntityTypeBuilder<RideType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(255);
            builder.Property(x => x.BaseFare).HasPrecision(18, 2);
            builder.Property(x => x.PerMinuteRate).HasPrecision(18, 2);
            builder.Property(x => x.PerKilometerRate).HasPrecision(18, 2);

            builder.HasMany(x => x.Trips).WithOne(x => x.RideType).HasForeignKey(x => x.RideTypeId);
        }
    }
}
