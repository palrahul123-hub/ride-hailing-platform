using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RideHailing.Application.Interfaces;
using RideHailing.Application.Profiles;
using RideHailing.Application.Services;

namespace RideHailing.Application
{
    public static class ApplicationServices
    {
        public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(UserProfiles).Assembly);
            services.AddAutoMapper(typeof(TripProfiles).Assembly);

            services.AddScoped<ITripService, TripService>();

        }
    }
}
