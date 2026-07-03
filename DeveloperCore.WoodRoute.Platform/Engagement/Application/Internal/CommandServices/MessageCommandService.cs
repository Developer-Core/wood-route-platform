using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.OutboundServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Acl;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Application.Internal.CommandServices;

/// <summary>
///     Handles message sending commands for order conversations.
/// </summary>
/// <remarks>
///     The tracking conversation is normally created at order creation. If none exists yet for the
///     given order, one is created here seeded with the order's real public tracking id (resolved via
///     the Sales anti-corruption facade) rather than a fresh, unrelated GUID, so public tracking keeps
///     working.
/// </remarks>
/// <param name="conversationRepository">
///     The conversation repository.
/// </param>
/// <param name="salesContextFacade">
///     The Sales anti-corruption facade used to resolve the order's public tracking id when seeding
///     a fallback conversation.
/// </param>
/// <param name="unitOfWork">
///     The unit of work.
/// </param>
/// <param name="notificationService">
///     The notification service used to notify the other party about new messages.
/// </param>
public class MessageCommandService(
    IConversationRepository conversationRepository,
    ISalesContextFacade salesContextFacade,
    IUnitOfWork unitOfWork,
    INotificationService notificationService) : IMessageCommandService
{
    /// <inheritdoc />
    public async Task<Result<Message>> Handle(SendMessageCommand command, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Content))
            return Result<Message>.Failure(EngagementErrors.MessageContentEmpty);

        // Try to find an existing conversation for this order
        var conversation = await conversationRepository.FindByOrderIdAsync(command.OrderId, cancellationToken);

        if (conversation is null)
        {
            // No conversation yet — create one seeded with the order's real public tracking id so it
            // resolves the same id the client received. Never seed a fresh, unrelated GUID.
            var publicTrackingId = await salesContextFacade.GetPublicTrackingIdByOrderIdAsync(command.OrderId);
            if (publicTrackingId is null)
                return Result<Message>.Failure(EngagementErrors.ConversationNotFound);

            conversation = new Conversation(command.OrderId, publicTrackingId.Value);
            await conversationRepository.AddAsync(conversation, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
        }

        var message = conversation.SendMessage(command.Content, command.SenderType, command.SenderId);

        conversationRepository.Update(conversation);
        await unitOfWork.CompleteAsync(cancellationToken);

        // Notify the other party about the new message (fire-and-forget on purpose)
        await notificationService.NotifyMessageSentAsync(conversation.Id, message, cancellationToken);

        return Result<Message>.Success(message);
    }
}
