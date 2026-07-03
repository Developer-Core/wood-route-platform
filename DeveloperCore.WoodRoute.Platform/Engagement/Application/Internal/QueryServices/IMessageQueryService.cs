using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Application.Internal.QueryServices;

/// <summary>
///     Contract for the message query service.
/// </summary>
public interface IMessageQueryService
{
    /// <summary>Returns paginated messages for an order conversation.</summary>
    Task<IEnumerable<Message>> Handle(GetMessagesQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns the conversation for unauthenticated tracking.
    ///     Returns null if the tracking id does not exist.
    /// </summary>
    Task<Conversation?> Handle(GetConversationByTrackingIdQuery query,
        CancellationToken cancellationToken = default);
}
