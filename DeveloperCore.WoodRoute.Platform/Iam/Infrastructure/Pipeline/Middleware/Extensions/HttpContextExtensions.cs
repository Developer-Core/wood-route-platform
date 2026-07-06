using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;

namespace DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;

/// <summary>
///     Extension methods for reading the authenticated identity resolved by
///     <see cref="Components.RequestAuthorizationMiddleware" /> and stored in
///     <c>HttpContext.Items["User"]</c>.
/// </summary>
/// <remarks>
///     Controllers must derive the acting user from this accessor (the JWT-backed identity) and
///     never from client-supplied input, so that object-level authorization cannot be bypassed.
/// </remarks>
public static class HttpContextExtensions
{
    /// <summary>
    ///     The <see cref="HttpContext.Items" /> key under which the authenticated user is stored.
    /// </summary>
    public const string UserItemKey = "User";

    /// <summary>
    ///     Returns the authenticated <see cref="User" /> resolved for the current request, or
    ///     <c>null</c> when the request is anonymous or the user could not be resolved.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <returns>The authenticated <see cref="User" />, or <c>null</c> when none is present.</returns>
    public static User? GetAuthenticatedUser(this HttpContext context)
    {
        return context.Items[UserItemKey] as User;
    }
}
