using DeveloperCore.WoodRoute.Platform.Shared.Domain.Events;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Domain.Events;

/// <summary>
///     Domain event raised when a message is successfully added to a conversation.
///     Used to notify the other party involved (client or workshop) about the new message.
/// </summary>
/// <param name="ConversationId">Id of the conversation where the message was sent.</param>
/// <param name="Content">Text content of the message.</param>
/// <param name="SenderType">Who sent it: "Client" or "Workshop".</param>
/// <param name="SenderId">Id of the user who sent the message.</param>
/// <param name="OccurredOn">UTC timestamp when the event occurred.</param>
public record MessageSentDomainEvent(
    int ConversationId,
    string Content,
    string SenderType,
    int SenderId,
    DateTimeOffset OccurredOn
) : IDomainEvent;
