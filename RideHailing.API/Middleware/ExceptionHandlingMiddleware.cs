using RideHailing.Application.Common;
using RideHailing.Application.CustomException;
using System.Net;
using System.Text.Json;

namespace RideHailing.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Unhandled Exception");



                var statusCode = ex switch
                {
                    BadRequestException => HttpStatusCode.BadRequest,
                    ForbiddenException => HttpStatusCode.Forbidden,
                    NotFoundException => HttpStatusCode.NotFound,
                    ConflictException => HttpStatusCode.Conflict,
                    _ => HttpStatusCode.InternalServerError
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;
                var errorResponse = APIResponse<object>.ErrorResponse(ex.Message);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, options));


            }
        }
    }
}
