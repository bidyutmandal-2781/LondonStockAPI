
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace LondonStockAPI.Exceptions
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                TraceId = context.TraceIdentifier,
                Instance = context.Request.Path,
                Timestamp = DateTime.UtcNow
            };

            switch (exception)
            {
                case ValidationException validationException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Title = "Validation Failed";
                    errorResponse.Status = (int)HttpStatusCode.BadRequest;
                    errorResponse.Errors = validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        );
                    _logger.LogWarning(validationException,
                        "Validation error occurred for {Path}. Errors: {Errors}",
                        context.Request.Path,
                        string.Join(", ", errorResponse.Errors.SelectMany(e => e.Value)));
                    break;

                case InvalidOperationException invalidOpException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Title = "Business Rule Violation";
                    errorResponse.Status = (int)HttpStatusCode.BadRequest;
                    errorResponse.Detail = invalidOpException.Message;
                    _logger.LogWarning(invalidOpException,
                        "Business rule violation at {Path}: {Message}",
                        context.Request.Path,
                        invalidOpException.Message);
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Title = "Internal Server Error";
                    errorResponse.Status = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Detail = "An unexpected error occurred. Please contact support.";

                    _logger.LogError(exception,
                        "Unhandled exception occurred at {Path}: {Message}",
                        context.Request.Path,
                        exception.Message);
                    break;
            }

            var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await response.WriteAsync(jsonResponse);
        }
    }
}
