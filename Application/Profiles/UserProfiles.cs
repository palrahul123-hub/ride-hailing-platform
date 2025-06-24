using AutoMapper;
using RideHailing.Application.DTOs;
using RideHailing.Domain.Entities;

namespace RideHailing.Application.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<User, UserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

            CreateMap<UserDto, User>();
        }
    }

}
