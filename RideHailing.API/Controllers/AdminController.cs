using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideHailing.Application.Common;
using RideHailing.Application.DTOs;
using RideHailing.Application.Interfaces;

namespace RideHailing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITripService _tripService;
        public AdminController(IUserService userService, ITripService tripService)
        {
            _tripService = tripService;
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<ActionResult<APIResponse<IEnumerable<UserDto>>>> GetAllUsers()
        {
            var users = await _userService.GetAllAysnc();
            return Ok(APIResponse<IEnumerable<UserDto>>.SuccessResponse(users, "All users"));
        }

        // GET: /api/admin/trips
        [HttpGet("trips")]
        public async Task<ActionResult<APIResponse<IEnumerable<TripDto>>>> GetAllTrips()
        {
            var trips = await _tripService.GetAllAsync();
            return Ok(APIResponse<IEnumerable<TripDto>>.SuccessResponse(trips, "All trips"));
        }

        [HttpPost("users/{id}/block")]
        public async Task<IActionResult> BlockUser(Guid id)
        {
            await _userService.BlockUserAsync(id);
            return Ok(APIResponse<object>.SuccessResponse(null, "User blocked"));
        }
        [HttpPost("users/{id}/UnBlockUser")]
        public async Task<IActionResult> UnBlockUser(Guid id)
        {
            await _userService.UnBlockUserAsync(id);
            return Ok(APIResponse<object>.SuccessResponse(null, "User Unblocked"));
        }
    }
}
