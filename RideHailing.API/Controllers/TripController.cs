using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideHailing.Application.Common;
using RideHailing.Application.CustomException;
using RideHailing.Application.DTOs;
using RideHailing.Application.Interfaces;
using System.Security.Claims;

namespace RideHailing.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;
        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }



        [HttpPost]
        public async Task<ActionResult<APIResponse<TripDto>>> Create([FromBody] TripDto tripDto)
        {
            if (string.IsNullOrEmpty(tripDto.PickupLocation) || string.IsNullOrEmpty(tripDto.DropoffLocation))
                throw new BadRequestException("Pickup and dropoff locations are required.");
            var data = await _tripService.CreateTripAsync(tripDto);
            var response = APIResponse<TripDto>.SuccessResponse(data, "Trip created");

            return CreatedAtRoute(nameof(GetById), new { id = data.Id }, response);

        }

        [HttpGet()]
        [Route("{id}")]
        public async Task<ActionResult<APIResponse<TripDto>>> GetById(Guid Id)
        {
            var trip = await _tripService.GetTripByIdAsync(Id);
            if (trip == null)
                throw new NotFoundException("Trip", Id);

            return Ok(APIResponse<TripDto>.SuccessResponse(trip, "Trip fetched"));
        }

        [HttpGet("/api/users/{userId}/trips")]
        public async Task<ActionResult<APIResponse<IEnumerable<TripDto>>>> GetByUser(Guid userId)
        {
            var trips = await _tripService.GetTripsByUserAsync(userId);
            if (trips.Any())
                throw new NotFoundException("User", userId);

            return Ok(APIResponse<IEnumerable<TripDto>>.SuccessResponse(trips, "User's trips fetched"));
        }

        [Authorize(Roles = "Driver")]
        [HttpPost("{tripId}/accept")]
        public async Task<ActionResult<APIResponse<TripDto>>> AcceptTrip(Guid tripId)
        {
            var driverId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var trip = await _tripService.AcceptTripAsync(tripId, driverId);
            return Ok(APIResponse<TripDto>.SuccessResponse(trip, "Trip accepted"));
        }

        [Authorize(Roles = "Driver")]
        [HttpPost("{tripId}/complete")]
        public async Task<ActionResult<APIResponse<TripDto>>> CompleteTrip(Guid tripId)
        {
            var driverId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var trip = await _tripService.CompleteTripAsync(tripId, driverId);
            return Ok(APIResponse<TripDto>.SuccessResponse(trip, "Trip completed"));
        }

        [Authorize(Roles = "Rider")]
        [HttpPost("{tripId}/cancel")]
        public async Task<ActionResult<APIResponse<TripDto>>> CancelTrip(Guid tripId)
        {
            var riderId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var trip = await _tripService.CancelTripAsync(tripId, riderId);
            return Ok(APIResponse<TripDto>.SuccessResponse(trip, "Trip cancelled"));
        }
    }
}
