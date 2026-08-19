using System.Net;
using System.Text.Json;
using TaskManagement.Exceptions;

namespace TaskManagement.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                NotFoundException => HttpStatusCode.NotFound,  //404
                DuplicateException => HttpStatusCode.Conflict, //409
                ValidationException => HttpStatusCode.BadRequest, //400
                BusinessException => HttpStatusCode.BadRequest,

                _ => HttpStatusCode.InternalServerError //500
            };

            var response = new
            {
                statusCode = (int)statusCode,
                message = exception.Message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
