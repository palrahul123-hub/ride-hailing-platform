using RideHailing.Application.DTOs;

namespace RideHailing.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterUserAsync(UserDto userDto);
        Task<UserDto> GetByIdAsync(Guid id);
        Task<IReadOnlyList<UserDto>> GetAllAysnc();
    }
}
