using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration.Extensions;

/// <summary>
///     Extension methods for configuring request localization in the application.
/// </summary>
public static class LocalizationExtensions
{
    /// <summary>
    ///     Registers localization services and configures the supported cultures.
    /// </summary>
    /// <remarks>
    ///     Supported cultures: <c>en</c> (default) and <c>es</c>.
    ///     The locale is resolved in the following order:
    ///     <list type="number">
    ///         <item>Query string parameter <c>?culture=es</c></item>
    ///         <item><c>Accept-Language</c> HTTP header</item>
    ///         <item>Default culture (<c>en</c>)</item>
    ///     </list>
    ///     Resource files must be placed under the <c>Resources/</c> folder and named
    ///     following the convention <c>ResourceName.{culture}.resx</c>.
    /// </remarks>
    /// <param name="services">The service collection.</param>
    /// <returns>The same <see cref="IServiceCollection" /> for chaining.</returns>
    public static IServiceCollection AddSharedLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("es")
            };

            options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.RequestCultureProviders =
            [
                new QueryStringRequestCultureProvider(),
                new AcceptLanguageHeaderRequestCultureProvider()
            ];
        });

        return services;
    }

    /// <summary>
    ///     Adds the request localization middleware to the application pipeline.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The same <see cref="IApplicationBuilder" /> for chaining.</returns>
    public static IApplicationBuilder UseSharedLocalization(this IApplicationBuilder app)
    {
        app.UseRequestLocalization();
        return app;
    }
}
