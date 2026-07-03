using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DeveloperCore.WoodRoute.Platform.Shared.Interfaces.Rest.ProblemDetails;

/// <summary>
///     Builds localized <see href="https://www.rfc-editor.org/rfc/rfc7807">RFC 7807</see>
///     problem details responses from domain <see cref="Error" /> instances.
/// </summary>
/// <remarks>
///     The message is resolved from the shared resource files keyed by the error
///     <see cref="Error.Code" />. When no resource entry exists for the code (or when a
///     parameterized template is resolved without its arguments), the hardcoded English
///     <see cref="Error.Message" /> is used as the fallback, guaranteeing a meaningful response.
/// </remarks>
/// <param name="localizer">The shared resource localizer used to resolve error messages.</param>
/// <param name="problemDetailsFactory">The ASP.NET Core factory used to build the base problem details.</param>
public class ProblemDetailsFactory(
    IStringLocalizer<SharedResource> localizer,
    Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory problemDetailsFactory)
{
    /// <summary>
    ///     Creates a localized problem details response for the given domain error.
    /// </summary>
    /// <param name="controller">The <see cref="ControllerBase" /> producing the response.</param>
    /// <param name="statusCode">The HTTP status code for the response.</param>
    /// <param name="error">The domain <see cref="Error" /> to render.</param>
    /// <param name="arguments">
    ///     Optional arguments used to format parameterized messages that contain
    ///     placeholders such as <c>{0}</c> in their resource value.
    /// </param>
    /// <returns>
    ///     An <see cref="IActionResult" /> rendering the error as a localized RFC 7807 problem details response.
    /// </returns>
    public IActionResult CreateFromError(ControllerBase controller, int statusCode, Error error,
        params object[] arguments)
    {
        var detail = LocalizeDetail(error, arguments);

        var problemDetails = problemDetailsFactory.CreateProblemDetails(
            controller.HttpContext,
            statusCode,
            error.Code,
            detail: detail,
            instance: controller.HttpContext.Request.Path);

        return controller.StatusCode(statusCode, problemDetails);
    }

    private string LocalizeDetail(Error error, object[] arguments)
    {
        var localized = arguments.Length > 0
            ? localizer[error.Code, arguments]
            : localizer[error.Code];

        // Fall back to the hardcoded English message when the resource is missing or when a
        // parameterized template was resolved without the arguments needed to fill it.
        if (localized.ResourceNotFound || (arguments.Length == 0 && localized.Value.Contains("{0}")))
            return error.Message;

        return localized.Value;
    }
}
