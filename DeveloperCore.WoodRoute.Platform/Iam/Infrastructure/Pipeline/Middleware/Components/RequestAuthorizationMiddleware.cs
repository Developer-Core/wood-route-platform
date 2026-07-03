using DeveloperCore.WoodRoute.Platform.Iam.Application.Internal.OutboundServices;
using DeveloperCore.WoodRoute.Platform.Iam.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Components;

/// <summary>
///     Custom middleware that authorizes every request by default.
/// </summary>
/// <remarks>
///     Endpoints decorated with <see cref="AllowAnonymousAttribute" /> are allowed through
///     without a token. For every other endpoint the middleware requires a valid JWT bearer
///     token in the <c>Authorization</c> header, resolves the corresponding user and stores it
///     in <c>HttpContext.Items["User"]</c>. A missing or invalid token raises an
///     <see cref="UnauthorizedAccessException" />, translated to a 401 response by the global
///     exception handler.
/// </remarks>
/// <param name="next">The next middleware in the pipeline.</param>
public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    /// <summary>
    ///     Invokes the middleware.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <param name="userQueryService">The user query service used to resolve the authenticated user.</param>
    /// <param name="tokenService">The token service used to validate the JWT bearer token.</param>
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        // Skip authorization when the endpoint is decorated with [AllowAnonymous].
        var allowAnonymous = context.GetEndpoint()?.Metadata
            .Any(metadata => metadata.GetType() == typeof(AllowAnonymousAttribute)) ?? false;
        if (allowAnonymous)
        {
            await next(context);
            return;
        }

        // Extract the bearer token from the Authorization header.
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("Missing authentication token.");

        var userId = await tokenService.ValidateToken(token);
        if (userId is null)
            throw new UnauthorizedAccessException("Invalid authentication token.");

        var user = await userQueryService.Handle(new GetUserByIdQuery(userId.Value), context.RequestAborted);
        if (user is null)
            throw new UnauthorizedAccessException("Invalid authentication token.");

        context.Items["User"] = user;
        await next(context);
    }
}
