namespace RideHailing.API.CustomException
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message)
        {

        }
    }
}
