namespace RideHailing.API.CustomException
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message) { }
    }
}
