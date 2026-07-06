using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
///     Authorization filter that ensures a user has been resolved by
///     <c>RequestAuthorizationMiddleware</c> and stored in <c>HttpContext.Items["User"]</c>,
///     optionally enforcing that the user holds one of the required roles.
/// </summary>
/// <remarks>
///     Actions decorated with <see cref="AllowAnonymousAttribute" /> are skipped.
///     When no user is present a 401 Unauthorized response is produced. When one or more
///     roles are required and the authenticated user does not hold any of them a 403 Forbidden
///     response is produced, rendered as a localized RFC 7807 problem details payload.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly EUserRole[] _roles;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AuthorizeAttribute" /> class.
    /// </summary>
    /// <param name="roles">
    ///     The roles allowed to access the decorated endpoint. When empty, any authenticated
    ///     user is allowed.
    /// </param>
    public AuthorizeAttribute(params EUserRole[] roles)
    {
        _roles = roles;
    }

    /// <summary>
    ///     Enforces that an authenticated user is present in the request context and, when roles
    ///     are required, that the user holds one of them.
    /// </summary>
    /// <param name="context">The authorization filter context.</param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        var user = context.HttpContext.GetAuthenticatedUser();
        if (user is null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // No specific role required: any authenticated user is allowed.
        if (_roles.Length == 0 || _roles.Contains(user.Role))
            return;

        context.Result = BuildForbiddenResult(context);
    }

    /// <summary>
    ///     Builds a 403 Forbidden response rendering the insufficient-role error as a localized
    ///     RFC 7807 problem details payload.
    /// </summary>
    /// <param name="context">The authorization filter context.</param>
    /// <returns>The forbidden action result.</returns>
    private static ObjectResult BuildForbiddenResult(AuthorizationFilterContext context)
    {
        var error = IamErrors.InsufficientRole;
        var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<SharedResource>>();
        var localized = localizer[error.Code];
        var detail = localized.ResourceNotFound ? error.Message : localized.Value;

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = error.Code,
            Detail = detail,
            Instance = context.HttpContext.Request.Path
        };

        return new ObjectResult(problemDetails) { StatusCode = StatusCodes.Status403Forbidden };
    }
}
