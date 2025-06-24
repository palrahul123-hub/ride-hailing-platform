using Microsoft.Extensions.DependencyInjection;
using RideHailing.Application.Profiles;

namespace RideHailing.Application
{
    public static class ApplicationServices
    {
        public static void AddPplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfiles).Assembly);
            services.AddAutoMapper(typeof(TripProfiles).Assembly);
        }
    }
}
