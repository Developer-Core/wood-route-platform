using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Application.OutboundServices;

/// <summary>
///     Outbound port for sending notifications to users.
///     This is an interface because the actual implementation depends on an external
///     service (email, push notifications, etc.) that is outside this bounded context.
///     For now, the infrastructure layer provides a no-op implementation.
/// </summary>
public interface INotificationService
{
    /// <summary>
    ///     Sends a notification to alert the recipient about a new message in the conversation.
    /// </summary>
    /// <param name="conversationId">Id of the conversation where the message was sent.</param>
    /// <param name="message">The message that was just sent.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task NotifyMessageSentAsync(int conversationId, Message message,
        CancellationToken cancellationToken = default);
}
