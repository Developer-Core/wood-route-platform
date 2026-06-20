using DeveloperCore.WoodRoute.Platform.Engagement.Application.Internal.CommandServices.Commands;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.OutboundServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Application.Internal.CommandServices;

/// <summary>
///     Handles message sending commands for order conversations.
/// </summary>
/// <remarks>
///     If a conversation does not exist yet for the given order, one is automatically created.
///     This avoids requiring a separate step to open a conversation before sending the first message.
/// </remarks>
public class MessageCommandService(
    IConversationRepository conversationRepository,
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
            // No conversation yet — create one automatically when the first message is sent
            conversation = new Conversation(command.OrderId);
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
