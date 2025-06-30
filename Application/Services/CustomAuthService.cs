using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RideHailing.Application.DTOs.LoginDto;
using RideHailing.Application.Interfaces;
using RideHailing.Application.JWTAuthentication;
using RideHailing.Domain.Entities;
using RideHailing.Infrastructure.DataBaseContext;

namespace RideHailing.Application.Services
{
    public class CustomAuthService : IAuthService
    {
        private readonly RideHandlingDBContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly JwtTokenGenerator _jwtGenerator;
        private readonly PasswordHasher<User> _hasher;
        public CustomAuthService(RideHandlingDBContext dBContext, IConfiguration configuration, JwtTokenGenerator jwtTokenGenerator, PasswordHasher<User> hasher)
        {
            _dbContext = dBContext;
            _configuration = configuration;
            _jwtGenerator = jwtTokenGenerator;
            _hasher = hasher;
        }
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(r => r.Email == loginRequestDto.Email);
            if (user == null)
                throw new Exception("User Not Found");

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, loginRequestDto.Password);


            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid password.");

            var token = _jwtGenerator.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(2)
            };
        }
    }
}
