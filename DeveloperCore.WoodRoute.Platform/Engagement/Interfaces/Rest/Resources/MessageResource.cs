namespace DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Resources;

/// <summary>
///     Response DTO for a single message in a conversation.
/// </summary>
/// <param name="Id">Unique message identifier.</param>
/// <param name="Content">Text content of the message.</param>
/// <param name="SenderType">Who sent it: "Client" or "Workshop".</param>
/// <param name="SenderId">Id of the user who sent the message.</param>
/// <param name="SentAt">UTC timestamp when the message was sent.</param>
public record MessageResource(int Id, string Content, string SenderType, int SenderId, DateTimeOffset SentAt);
