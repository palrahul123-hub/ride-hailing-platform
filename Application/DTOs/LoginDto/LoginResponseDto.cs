﻿namespace RideHailing.Application.DTOs.LoginDto
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

}
