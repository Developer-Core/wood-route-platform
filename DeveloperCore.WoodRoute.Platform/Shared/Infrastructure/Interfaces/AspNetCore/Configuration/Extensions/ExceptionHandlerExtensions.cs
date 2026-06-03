using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Middleware;

namespace DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration.Extensions;

/// <summary>
///     Extension methods for registering the global exception handler middleware.
/// </summary>
public static class ExceptionHandlerExtensions
{
    /// <summary>
    ///     Adds the <see cref="GlobalExceptionHandlerMiddleware" /> to the application's request pipeline.
    /// </summary>
    /// <remarks>
    ///     This should be registered as early as possible in the pipeline so that it can
    ///     catch exceptions thrown by any subsequent middleware or controller action.
    /// </remarks>
    /// <param name="app">The application builder.</param>
    /// <returns>The same <see cref="IApplicationBuilder" /> for chaining.</returns>
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        return app;
    }
}
