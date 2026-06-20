using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Infrastructure.OutboundServices;

/// <summary>
///     No-operation implementation of <see cref="Application.OutboundServices.INotificationService" />.
///     Used during development until a real notification provider (email, push, etc.) is integrated.
/// </summary>
public class NoOpNotificationService : Application.OutboundServices.INotificationService
{
    public Task NotifyMessageSentAsync(int conversationId, Message message,
        CancellationToken cancellationToken = default)
    {
        // Intentionally does nothing — replace this with the real implementation when ready
        return Task.CompletedTask;
    }
}
