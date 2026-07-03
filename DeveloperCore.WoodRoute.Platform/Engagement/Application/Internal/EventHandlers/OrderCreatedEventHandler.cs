using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Events;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Acl;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Events;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Application.Internal.EventHandlers;

/// <summary>
///     Creates the public tracking conversation as soon as an order is created.
/// </summary>
/// <remarks>
///     The conversation adopts the order's <see cref="OrderCreatedEvent.PublicTrackingId" /> so the
///     unauthenticated tracking endpoint resolves the very id the client received when placing the
///     order, and it exists before any message is sent so the stage-progress sync has a conversation
///     to update. The int order id is DB-generated and is 0 in the event (built in the Order
///     constructor), so the real id is resolved from the Sales context using the stable tracking id.
///     This is a one-directional Engagement -> Sales reference through the anti-corruption facade,
///     mirroring the existing <c>StageUpdatedEventHandler</c> pattern.
/// </remarks>
/// <param name="conversationRepository">
///     The conversation repository used to create the tracking conversation.
/// </param>
/// <param name="salesContextFacade">
///     The Sales anti-corruption facade used to resolve the persisted order id.
/// </param>
/// <param name="unitOfWork">
///     The unit of work used to persist the new conversation.
/// </param>
public class OrderCreatedEventHandler(
    IConversationRepository conversationRepository,
    ISalesContextFacade salesContextFacade,
    IUnitOfWork unitOfWork) : IDomainEventHandler<OrderCreatedEvent>
{
    /// <inheritdoc />
    public async Task HandleAsync(OrderCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // Resolve the real order id from the tracking id, since the event's OrderId is 0 at creation.
        var orderId = await salesContextFacade.GetOrderIdByPublicTrackingIdAsync(domainEvent.PublicTrackingId);
        if (orderId is null) return;

        // Idempotency: skip if a conversation already exists for this order.
        var existing = await conversationRepository.FindByOrderIdAsync(orderId.Value, cancellationToken);
        if (existing is not null) return;

        var conversation = new Conversation(orderId.Value, domainEvent.PublicTrackingId);
        await conversationRepository.AddAsync(conversation, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
