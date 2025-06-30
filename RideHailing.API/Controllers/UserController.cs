using Microsoft.AspNetCore.Mvc;
using RideHailing.Application.Common;
using RideHailing.Application.CustomException;
using RideHailing.Application.DTOs;
using RideHailing.Application.Interfaces;

namespace RideHailing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Register([FromBody] UserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.FullName) || string.IsNullOrWhiteSpace(userDto.Email))
                throw new BadRequestException("Full name and email are required.");

            var created = await _userService.RegisterUserAsync(userDto);
            var response = APIResponse<UserDto>.SuccessResponse(created, "User registered");

            return CreatedAtRoute(nameof(GetById), new { id = created.Id }, response);

        }

        // GET: /api/users/{id}
        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<APIResponse<UserDto>>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                throw new NotFoundException("User", id);

            return Ok(APIResponse<UserDto>.SuccessResponse(user, "User found"));
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse<IEnumerable<UserDto>>>> GetAll()
        {
            var users = await _userService.GetAllAysnc();
            return Ok(APIResponse<IEnumerable<UserDto>>.SuccessResponse(users, "All users fetched"));
        }
    }
}
