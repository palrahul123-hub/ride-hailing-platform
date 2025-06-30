namespace RideHailing.Application.CustomException
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message, object key) : base($"{message} with ID '{key}' was not found")
        {

        }
    }
}
