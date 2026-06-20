using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;

/// <summary>
///     Represents a single message exchanged between the client and the workshop.
///     Belongs to a <see cref="Aggregates.Conversation" /> and is always created through it.
/// </summary>
public class Message : IAuditableEntity
{
    // EF Core requires this parameterless constructor to materialise entities from the database
    private Message()
    {
        Content = string.Empty;
        SenderType = string.Empty;
    }

    public Message(int conversationId, string content, string senderType, int senderId)
    {
        ConversationId = conversationId;
        Content = content;
        SenderType = senderType;
        SenderId = senderId;
        SentAt = DateTimeOffset.UtcNow;
    }

    public int Id { get; private set; }
    public int ConversationId { get; private set; }

    /// <summary>Text content of the message.</summary>
    public string Content { get; private set; }

    /// <summary>
    ///     Identifies who sent the message. Expected values: "Client" or "Workshop".
    ///     We keep it as a string instead of an enum to make it easier to extend later.
    /// </summary>
    public string SenderType { get; private set; }

    /// <summary>Id of the user (client or workshop member) who sent this message.</summary>
    public int SenderId { get; private set; }

    /// <summary>UTC timestamp when the message was sent.</summary>
    public DateTimeOffset SentAt { get; private set; }

    // From IAuditableEntity — managed automatically by AuditableEntityInterceptor
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
