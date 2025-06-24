using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideHailing.Domain.Entities;

namespace RideHailing.Infrastructure.EntityConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Role).IsRequired();

            builder.HasMany(x => x.Trips).WithOne(x => x.Rider).HasForeignKey(x => x.RiderId);
            builder.HasMany(x => x.PaymentMethods).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            //Test By Rahul Pal
        }
    }
}
