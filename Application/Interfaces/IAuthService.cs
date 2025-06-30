using RideHailing.Application.DTOs.LoginDto;

namespace RideHailing.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
