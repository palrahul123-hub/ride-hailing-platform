using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RideHailing.Application.CustomException;
using RideHailing.Application.DTOs;
using RideHailing.Application.Interfaces;
using RideHailing.Domain.Entities;
using RideHailing.Domain.Enums;
using RideHailing.Infrastructure.DataBaseContext;

namespace RideHailing.Application.Services
{
    public class TripService : ITripService
    {
        private readonly RideHandlingDBContext _dbContext;
        private readonly IMapper _mapper;

        public TripService(RideHandlingDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TripDto> AcceptTripAsync(Guid tripId, Guid driverId)
        {
            var trip = await _dbContext.Trips.Include(r => r.RideType).FirstOrDefaultAsync(t => t.Id == tripId);
            if (trip == null)
                throw new NotFoundException("Trip", tripId);

            if (trip.Status != Domain.Enums.TripStatus.Accepted)
                throw new ConflictException("Trip is already accepted or in progress.");

            trip.DriverId = driverId;
            trip.Status = TripStatus.Accepted;
            trip.StartTime = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TripDto>(trip);


        }

        public async Task<TripDto> CancelTripAsync(Guid tripId, Guid riderId)
        {
            var trip = await _dbContext.Trips.FirstOrDefaultAsync(t => t.Id == tripId);

            if (trip == null || trip.RiderId != riderId)
                throw new NotFoundException("Trip", tripId);

            if (trip.Status != TripStatus.Requested)
                throw new ConflictException("Only pending trips can be cancelled.");

            trip.Status = TripStatus.Cancelled;

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<TripDto>(trip);
        }

        public async Task<TripDto> CompleteTripAsync(Guid tripId, Guid driverId)
        {
            var trip = await _dbContext.Trips.Include(t => t.RideType).FirstOrDefaultAsync(t => t.Id == tripId);

            if (trip == null || trip.DriverId != driverId)
                throw new NotFoundException("Trip", tripId);

            if (trip.Status != TripStatus.InProgress && trip.Status != TripStatus.Accepted)
                throw new ConflictException("Trip cannot be completed in current state.");

            trip.Status = TripStatus.Completed;
            trip.EndTime = DateTime.UtcNow;
            trip.Fare = CalculateFare(trip);

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<TripDto>(trip);
        }
        private decimal CalculateFare(Trip trip)
        {
            // Placeholder logic — replace with real distance/time
            return trip.RideType.BaseFare + 10;
        }
        public async Task<TripDto> CreateTripAsync(TripDto tripDto)
        {
            var trip = _mapper.Map<Trip>(tripDto);
            trip.Id = Guid.NewGuid();
            trip.Status = Domain.Enums.TripStatus.Requested;

            _dbContext.Add(trip);
            await _dbContext.SaveChangesAsync();

            trip = await _dbContext.Trips.Include(r => r.RideType).FirstOrDefaultAsync(t => t.Id == trip.Id);

            return _mapper.Map<TripDto>(trip);
        }

        public async Task<TripDto?> GetTripByIdAsync(Guid tripId)
        {
            var data = await _dbContext.Trips.Include(r => r.RideType).FirstOrDefaultAsync(t => t.Id == tripId);
            return _mapper.Map<TripDto>(data);
        }

        public async Task<IEnumerable<TripDto>> GetTripsByUserAsync(Guid userId)
        {
            var data = await _dbContext.Trips.Include(r => r.RideType).Where(t => t.RiderId == userId).ToListAsync();
            return _mapper.Map<IEnumerable<TripDto>>(data);
        }

        public async Task<IEnumerable<TripDto>> GetAllAsync()
        {
            var trips = await _dbContext.Trips.Include(r => r.RideType).ToListAsync();
            return _mapper.Map<IEnumerable<TripDto>>(trips);
        }
    }
}
