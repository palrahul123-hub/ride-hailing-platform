using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RideHailing.Application.Interfaces;
using RideHailing.Application.Profiles;
using RideHailing.Application.Services;
using RideHailing.Domain.Entities;
using System.Text;

namespace RideHailing.Application
{
    public static class ApplicationServices
    {
        public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(UserProfiles).Assembly);
            services.AddAutoMapper(typeof(TripProfiles).Assembly);

            services.AddScoped<ITripService, TripService>();
            services.AddScoped<IUserService, UserService>();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
            )
                    };

                });
            services.AddScoped<PasswordHasher<User>>();
        }
    }
}
