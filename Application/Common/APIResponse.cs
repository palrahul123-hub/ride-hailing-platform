namespace RideHailing.Application.Common
{
    public class APIResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static APIResponse<T> SuccessResponse(T data, string? message = null) => new APIResponse<T>
        {
            Success = true,
            Data = data,
            Message = message
        };

        public static APIResponse<T> ErrorResponse(string Message) => new()
        {
            Success = false,
            Message = Message
        };


    }
}
