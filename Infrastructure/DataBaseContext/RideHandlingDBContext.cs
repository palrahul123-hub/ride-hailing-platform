using Microsoft.EntityFrameworkCore;
using RideHailing.Domain.Entities;
using RideHailing.Infrastructure.SeedData;

namespace RideHailing.Infrastructure.DataBaseContext
{
    public class RideHandlingDBContext : DbContext
    {
        public RideHandlingDBContext(DbContextOptions<RideHandlingDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<RideType> RideTypes { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RideHandlingDBContext).Assembly);

            //Seed Data
            RideTypeSeed.Seed(modelBuilder);
        }

    }
}
