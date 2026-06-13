using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecruitProApp.Domain.Common;

namespace RecruitProApp.WebAPI.Middleware
{
    /// <summary>
    /// Translates exceptions into RFC 7807 ProblemDetails responses:
    /// domain rule violations -> 400, missing resources -> 404, anything else -> 500.
    /// </summary>
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (status, title) = exception switch
            {
                DomainException => (StatusCodes.Status400BadRequest, "Business rule violation"),
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
            };

            if (status == StatusCodes.Status500InternalServerError)
                _logger.LogError(exception, "Unhandled exception");
            else
                _logger.LogWarning("Handled exception ({Status}): {Message}", status, exception.Message);

            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = status == StatusCodes.Status500InternalServerError
                    ? "An unexpected error occurred. Please try again later."
                    : exception.Message,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = status;
            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

            return true;
        }
    }
}
