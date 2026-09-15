using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using TaskManagement.DTOs;

namespace TaskManagement.Middleware
{
    public class AuthMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();
        public async Task HandleAsync(
            RequestDelegate next,
            HttpContext context,
            AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Challenged)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse
                {
                    Success = false,
                    Message = "Authentication is required.",
                    Data = null,
                    TraceId = context.TraceIdentifier
                };

                await context.Response.WriteAsJsonAsync(response);
                return;
            }

            if (authorizeResult.Forbidden)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse
                {
                    Success = false,
                    Message = "You are not authorized to access this resource.",
                    Data = null,
                    TraceId = context.TraceIdentifier
                };

                await context.Response.WriteAsJsonAsync(response);
                return;
            }

            await _defaultHandler.HandleAsync(
                next,
                context,
                policy,
                authorizeResult);
        }
    }
}
