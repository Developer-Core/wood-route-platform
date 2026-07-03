namespace DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
///     Marks a controller or action as publicly accessible, skipping the request
///     authorization enforced by <c>RequestAuthorizationMiddleware</c>.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute;
