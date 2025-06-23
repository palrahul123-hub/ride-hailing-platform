using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideHailing.Domain.Entities;

namespace RideHailing.Infrastructure.EntityConfiguration
{
    public class PaymentMethodEntityConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Last4).HasMaxLength(4);
            builder.Property(x => x.Provider).HasMaxLength(50);
            builder.Property(x => x.Token).HasMaxLength(50);


            builder.HasOne(x => x.User).WithMany(x => x.PaymentMethods).HasForeignKey(x => x.UserId);
        }
    }
}
