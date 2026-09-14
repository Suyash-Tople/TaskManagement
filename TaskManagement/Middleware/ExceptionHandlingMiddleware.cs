using System.Net;
using System.Text.Json;
using TaskManagement.DTOs;
using TaskManagement.Exceptions;

namespace TaskManagement.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                "Unhandled exception occurred. TraceId: {TraceId}, Path: {Path}",
                context.TraceIdentifier,
                context.Request.Path);

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

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = new ApiResponse
            {
                Success = false,
                Message = exception.Message,
                Data = null,
                TraceId = context.TraceIdentifier,
            };
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
