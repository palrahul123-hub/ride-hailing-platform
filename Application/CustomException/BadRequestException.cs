﻿namespace RideHailing.Application.CustomException
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
