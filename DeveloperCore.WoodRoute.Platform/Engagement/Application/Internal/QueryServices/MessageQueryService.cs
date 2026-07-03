using DeveloperCore.WoodRoute.Platform.Engagement.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Application.Internal.QueryServices;

/// <summary>
///     Handles read queries for the Engagement bounded context.
/// </summary>
/// <param name="conversationRepository">
///     The conversation repository.
/// </param>
public class MessageQueryService(IConversationRepository conversationRepository) : IMessageQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Message>> Handle(GetMessagesQuery query,
        CancellationToken cancellationToken = default)
    {
        // If there is no conversation for this order, return an empty list instead of 404
        var conversation = await conversationRepository.FindByOrderIdAsync(query.OrderId, cancellationToken);
        if (conversation is null) return [];

        return await conversationRepository.GetMessagesByConversationAsync(
            conversation.Id, query.Limit, query.Before, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Conversation?> Handle(GetConversationByTrackingIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await conversationRepository.FindByPublicTrackingIdAsync(query.PublicTrackingId, cancellationToken);
    }
}
