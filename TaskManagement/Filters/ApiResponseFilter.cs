using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagement.DTOs;

namespace TaskManagement.Filters
{
    public class ApiResponseFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if(context.Result is ObjectResult objectResult)
            {
                if(objectResult.Value is ApiResponse)
                {
                    await next();
                    return;
                }

                var statusCode = objectResult.StatusCode ?? StatusCodes.Status200OK;

                var response = new ApiResponse
                {
                    Success = statusCode >= 200 && statusCode < 300,
                    Message = GetDefaultMessage(statusCode),
                    Data = objectResult.Value,
                    TraceId = context.HttpContext.TraceIdentifier
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = statusCode
                };

                await next();
            }
        }

        private static string GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                StatusCodes.Status200OK => "Request Completed Successfully.",
                StatusCodes.Status201Created => "Resource Created Successfully.",
                StatusCodes.Status204NoContent => "Request Completed Successfully.",
                _ => "Request Completed."
            };
        }
    }
}
