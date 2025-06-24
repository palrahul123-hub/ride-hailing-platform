using RideHailing.Application.DTOs;

namespace RideHailing.Application.Interfaces
{
    public interface ITripService
    {
        Task<TripDto> CreateTripAsync(TripDto tripDto);
        Task<TripDto?> GetTripByIdAsync(Guid tripId);
        Task<IEnumerable<TripDto>> GetTripsByUserAsync(Guid userId);
    }
}
