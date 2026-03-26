using BankingService.API.Exceptions;
using System.Text.Json;

namespace BankingService.API.Middleware
{
   public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BaseException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new
                {
                    statusCode = ex.StatusCode,
                    message = ex.Message,
                    timestamp = DateTime.UtcNow
                });

                await context.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new
                {
                    statusCode = 500,
                    message = "Internal Server Error",
                    timestamp = DateTime.UtcNow
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
