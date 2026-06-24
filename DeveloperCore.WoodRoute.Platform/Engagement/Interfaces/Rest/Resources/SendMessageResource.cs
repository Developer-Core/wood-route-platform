namespace DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Resources;

/// <summary>
///     Request body for sending a message in an order conversation.
/// </summary>
/// <param name="Content">Text content of the message (required, max 2000 chars).</param>
/// <param name="SenderType">Who is sending: "Client" or "Workshop".</param>
/// <param name="SenderId">Id of the authenticated user sending the message.</param>
public record SendMessageResource(string Content, string SenderType, int SenderId);
