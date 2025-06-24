using AutoMapper;
using RideHailing.Application.DTOs;
using RideHailing.Domain.Entities;

namespace RideHailing.Application.Profiles
{
    public class TripProfiles : Profile
    {
        public TripProfiles()
        {
            CreateMap<Trip, TripDto>()
                .ForMember(dest => dest.RideType, opt => opt.MapFrom(src => src.RideType.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<TripDto, Trip>();
        }
    }
}
