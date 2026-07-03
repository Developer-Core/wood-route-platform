using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Components;

namespace DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;

/// <summary>
///     Extension methods for registering <see cref="RequestAuthorizationMiddleware" /> in the pipeline.
/// </summary>
public static class RequestAuthorizationMiddlewareExtensions
{
    /// <summary>
    ///     Adds the <see cref="RequestAuthorizationMiddleware" /> to the application's request pipeline.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <returns>The same <see cref="IApplicationBuilder" /> for chaining.</returns>
    public static IApplicationBuilder UseRequestAuthorization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestAuthorizationMiddleware>();
    }
}
