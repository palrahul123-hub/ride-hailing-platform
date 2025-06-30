using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RideHailing.Application.CustomException;
using RideHailing.Application.DTOs;
using RideHailing.Application.Interfaces;
using RideHailing.Domain.Entities;
using RideHailing.Domain.Enums;
using RideHailing.Infrastructure.DataBaseContext;

namespace RideHailing.Application.Services
{
    public class UserService : IUserService
    {
        private readonly RideHandlingDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<User> _passwordHasher;
        public UserService(RideHandlingDBContext dbContext, IMapper mapper, PasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<IReadOnlyList<UserDto>> GetAllAysnc()
        {
            var users = await _dbContext.Users.ToListAsync();
            return _mapper.Map<IReadOnlyList<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<UserDto> RegisterUserAsync(UserDto userDto)
        {
            if (await _dbContext.Users.AnyAsync(r => r.Email == userDto.Email))
                throw new Exception("User with this email already exists.");

            var user = _mapper.Map<User>(userDto);
            user.Id = Guid.NewGuid();

            user.Role = Enum.TryParse<UserRole>(userDto.Role, true, out var role) ? role : UserRole.RIDER;
            user.PasswordHash = _passwordHasher.HashPassword(user, userDto.Password);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task BlockUserAsync(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
                throw new NotFoundException("User", userId);

            user.IsBlocked = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UnBlockUserAsync(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
                throw new NotFoundException("User", userId);

            user.IsBlocked = false;
            await _dbContext.SaveChangesAsync();
        }
    }
}
