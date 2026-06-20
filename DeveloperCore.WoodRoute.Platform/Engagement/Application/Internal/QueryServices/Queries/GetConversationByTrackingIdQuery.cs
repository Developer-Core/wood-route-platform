namespace DeveloperCore.WoodRoute.Platform.Engagement.Application.Internal.QueryServices.Queries;

/// <summary>
///     Query to retrieve conversation data using the public tracking id.
///     Used by the unauthenticated tracking endpoint.
/// </summary>
/// <param name="PublicTrackingId">The public GUID shared with the client for order tracking.</param>
public record GetConversationByTrackingIdQuery(Guid PublicTrackingId);
