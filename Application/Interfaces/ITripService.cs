using RideHailing.Application.DTOs;

namespace RideHailing.Application.Interfaces
{
    public interface ITripService
    {
        Task<TripDto> CreateTripAsync(TripDto tripDto);
        Task<TripDto?> GetTripByIdAsync(Guid tripId);
        Task<IEnumerable<TripDto>> GetTripsByUserAsync(Guid userId);
        Task<IEnumerable<TripDto>> GetAllAsync();
        Task<TripDto> AcceptTripAsync(Guid tripId, Guid driverId);
        Task<TripDto> CancelTripAsync(Guid tripId, Guid riderId);
        Task<TripDto> CompleteTripAsync(Guid tripId, Guid driverId);

    }
}
