using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Domain.Repositories;

/// <summary>
///     Repository contract for the <see cref="Conversation" /> aggregate.
///     Extends base CRUD operations with engagement-specific queries.
/// </summary>
public interface IConversationRepository : IBaseRepository<Conversation>
{
    /// <summary>
    ///     Finds a conversation using the public tracking id.
    ///     Used by the unauthenticated tracking endpoint.
    /// </summary>
    Task<Conversation?> FindByPublicTrackingIdAsync(Guid publicTrackingId,
        CancellationToken cancellationToken = default);

    /// <summary>Finds the conversation linked to a specific order.</summary>
    Task<Conversation?> FindByOrderIdAsync(int orderId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns a paginated slice of messages for a conversation in descending chronological order.
    ///     The <paramref name="before" /> cursor allows the client to fetch older messages page by page.
    /// </summary>
    Task<IEnumerable<Message>> GetMessagesByConversationAsync(
        int conversationId,
        int limit,
        DateTimeOffset? before,
        CancellationToken cancellationToken = default);
}
