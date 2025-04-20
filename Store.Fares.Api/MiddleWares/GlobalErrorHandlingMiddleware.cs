using Domain.Exceptions;
using Shared.ErrorsModels;

namespace Store.Fares.Api.MiddleWares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next,ILogger<GlobalErrorHandlingMiddleware> logger )
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                    await handlingNotFoundEndPointAsync(context);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                await HandlingErrorAsync(context, ex);

            }
        }

        private static async Task HandlingErrorAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails()
            {
                StatuseCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = ex.Message
            };

            response.StatuseCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = response.StatuseCode;

            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task handlingNotFoundEndPointAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatuseCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"End Point {context.Request.Path} is Not Found"
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
