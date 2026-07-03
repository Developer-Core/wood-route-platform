using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
///     Authorization filter that ensures a user has been resolved by
///     <c>RequestAuthorizationMiddleware</c> and stored in <c>HttpContext.Items["User"]</c>.
/// </summary>
/// <remarks>
///     Actions decorated with <see cref="AllowAnonymousAttribute" /> are skipped.
///     When no user is present a 401 Unauthorized response is produced.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    ///     Enforces that an authenticated user is present in the request context.
    /// </summary>
    /// <param name="context">The authorization filter context.</param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        var user = (User?)context.HttpContext.Items["User"];
        if (user is null)
            context.Result = new UnauthorizedResult();
    }
}
