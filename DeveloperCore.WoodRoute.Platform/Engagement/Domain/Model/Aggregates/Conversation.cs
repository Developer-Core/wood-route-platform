using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Events;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Aggregates;

/// <summary>
///     Aggregate root for order tracking and messaging.
///     Groups all messages of an order conversation and tracks the current production stage.
/// </summary>
/// <remarks>
///     The <see cref="PublicTrackingId" /> is a GUID shared with the client so they can
///     query their order status without needing an account (unauthenticated access).
///     All state changes go through methods on this aggregate to keep the business rules
///     in one place and avoid scattered logic across the application.
/// </remarks>
public class Conversation : AggregateRoot, IAuditableEntity
{
    private readonly List<Message> _messages = [];

    // EF Core requires a parameterless constructor to build the object from the database rows
    private Conversation()
    {
    }

    public Conversation(int orderId, DateTimeOffset? estimatedDeliveryDate = null)
    {
        OrderId = orderId;
        // Generate a public-facing GUID so the client can track their order without auth
        PublicTrackingId = Guid.NewGuid();
        CurrentStage = EProductionStage.NotStarted;
        EstimatedDeliveryDate = estimatedDeliveryDate;
        ProgressPercent = 0;
    }

    public int Id { get; private set; }

    /// <summary>
    ///     Shared with the client to allow unauthenticated order tracking via
    ///     <c>GET /api/v1/tracking/{publicTrackingId}</c>.
    /// </summary>
    public Guid PublicTrackingId { get; private set; }

    /// <summary>Foreign key reference to the order in the Sales bounded context.</summary>
    public int OrderId { get; private set; }

    /// <summary>Current stage of production in the workshop.</summary>
    public EProductionStage CurrentStage { get; private set; }

    /// <summary>Estimated date when the order will be ready for delivery.</summary>
    public DateTimeOffset? EstimatedDeliveryDate { get; private set; }

    /// <summary>Overall completion percentage, clamped between 0 and 100.</summary>
    public int ProgressPercent { get; private set; }

    /// <summary>Read-only view of all messages in this conversation.</summary>
    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    // From IAuditableEntity
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    ///     Adds a new message to the conversation and raises <see cref="MessageSentDomainEvent" />.
    ///     The event can be handled to notify the other party via email or push notification.
    /// </summary>
    public Message SendMessage(string content, string senderType, int senderId)
    {
        if (string.IsNullOrWhiteSpace(content))
            return null!; // Caller should validate content before calling this

        var message = new Message(Id, content, senderType, senderId);
        _messages.Add(message);

        // Raise the event so infrastructure can notify the other party asynchronously
        RaiseDomainEvent(new MessageSentDomainEvent(Id, content, senderType, senderId, DateTimeOffset.UtcNow));

        return message;
    }

    /// <summary>
    ///     Updates the production stage and recalculates the overall progress.
    ///     The percentage is clamped to [0, 100] to prevent invalid values from reaching the database.
    /// </summary>
    public void UpdateProgress(EProductionStage stage, int progressPercent, DateTimeOffset? estimatedDeliveryDate = null)
    {
        CurrentStage = stage;
        ProgressPercent = Math.Clamp(progressPercent, 0, 100);

        if (estimatedDeliveryDate.HasValue)
            EstimatedDeliveryDate = estimatedDeliveryDate;
    }
}
