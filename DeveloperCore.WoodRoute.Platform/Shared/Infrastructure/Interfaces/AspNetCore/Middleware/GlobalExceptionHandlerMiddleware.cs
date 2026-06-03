using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Net;
using System.Text.Json;

namespace DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Middleware;

/// <summary>
///     Middleware that intercepts unhandled exceptions and returns a standardised
///     <see href="https://www.rfc-editor.org/rfc/rfc7807">RFC 7807 ProblemDetails</see> JSON response.
/// </summary>
/// <remarks>
///     Exception-to-status mapping:
///     <list type="table">
///         <listheader><term>Exception</term><description>HTTP Status</description></listheader>
///         <item><term><see cref="ArgumentNullException" />, <see cref="ArgumentException" /></term><description>400 Bad Request</description></item>
///         <item><term><see cref="KeyNotFoundException" /></term><description>404 Not Found</description></item>
///         <item><term><see cref="UnauthorizedAccessException" /></term><description>401 Unauthorized</description></item>
///         <item><term>Any other</term><description>500 Internal Server Error</description></item>
///     </list>
/// </remarks>
public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlerMiddleware> logger,
    IStringLocalizer<SharedResource> localizer)
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>
    ///     Invokes the middleware.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled exception caught by GlobalExceptionHandlerMiddleware: {Message}",
                exception.Message);

            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title, detail) = MapException(exception);

        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };

        problemDetails.Extensions["traceId"] = context.TraceIdentifier;

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, JsonOptions));
    }

    private (HttpStatusCode status, string title, string detail) MapException(Exception exception)
    {
        return exception switch
        {
            ArgumentNullException or ArgumentException =>
            (
                HttpStatusCode.BadRequest,
                localizer["Error.BadRequest"].Value,
                exception.Message
            ),
            KeyNotFoundException =>
            (
                HttpStatusCode.NotFound,
                localizer["Error.NotFound"].Value,
                exception.Message
            ),
            UnauthorizedAccessException =>
            (
                HttpStatusCode.Unauthorized,
                localizer["Error.Unauthorized"].Value,
                exception.Message
            ),
            _ =>
            (
                HttpStatusCode.InternalServerError,
                localizer["Error.InternalServer"].Value,
                localizer["Error.InternalServer"].Value
            )
        };
    }
}
