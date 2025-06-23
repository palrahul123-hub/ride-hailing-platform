using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RideHailing.Infrastructure.DataBaseContext;

namespace RideHailing.Infrastructure
{
    public static class ServiceConfiguration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RideHandlingDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("RideAppConnectionString"));
            });
        }
    }
}
