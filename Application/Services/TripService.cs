using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RideHailing.Application.DTOs;
using RideHailing.Application.Interfaces;
using RideHailing.Domain.Entities;
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
    }
}
