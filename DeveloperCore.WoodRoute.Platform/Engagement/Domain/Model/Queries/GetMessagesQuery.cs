namespace DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Queries;

/// <summary>
///     Query to retrieve a paginated list of messages for a specific order conversation.
/// </summary>
/// <param name="OrderId">Id of the order whose messages are being requested.</param>
/// <param name="Limit">Maximum number of messages to return (default: 20).</param>
/// <param name="Before">
///     Cursor for pagination. Returns messages sent before this timestamp.
///     If null, returns the most recent messages.
/// </param>
public record GetMessagesQuery(int OrderId, int Limit = 20, DateTimeOffset? Before = null);
