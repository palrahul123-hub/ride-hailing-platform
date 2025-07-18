﻿using Microsoft.AspNetCore.Mvc;
using RideHailing.Application.Common;
using RideHailing.Application.CustomException;
using RideHailing.Application.DTOs.LoginDto;
using RideHailing.Application.Interfaces;

namespace RideHailing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            LoginResponseDto response = await _authService.LoginAsync(loginRequestDto);

            if (response.IsBlocked == true)
                throw new ForbiddenException("User is blocked.");
            return Ok(APIResponse<LoginResponseDto>.SuccessResponse(response, "Login successful"));

        }
    }
}
