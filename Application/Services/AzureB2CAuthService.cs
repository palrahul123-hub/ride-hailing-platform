using RideHailing.Application.DTOs.LoginDto;
using RideHailing.Application.Interfaces;

namespace RideHailing.Application.Services
{
    public class AzureB2CAuthService : IAuthService
    {
        public Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
