namespace DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Commands;

/// <summary>
///     Command to send a new message in the conversation linked to a specific order.
/// </summary>
/// <param name="OrderId">Id of the order whose conversation receives the message.</param>
/// <param name="Content">Text content of the message to send.</param>
/// <param name="SenderType">Who is sending: "Client" or "Workshop".</param>
/// <param name="SenderId">Id of the authenticated user sending the message.</param>
public record SendMessageCommand(int OrderId, string Content, string SenderType, int SenderId);
