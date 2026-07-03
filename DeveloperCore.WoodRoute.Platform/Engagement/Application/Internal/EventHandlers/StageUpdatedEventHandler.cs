using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Events;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Events;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Application.Internal.EventHandlers;

/// <summary>
///     Keeps the public tracking conversation in sync with the manufacturing progress.
/// </summary>
/// <remarks>
///     When a production stage is updated in the Manufacturing context, this handler locates the
///     conversation linked to the same sales order and updates its current stage and progress so
///     the unauthenticated tracking endpoint reflects the real production state. This is a
///     one-directional Engagement -> Manufacturing reference, accepted for event integration.
/// </remarks>
/// <param name="conversationRepository">
///     The conversation repository used to find and update the tracking conversation.
/// </param>
/// <param name="unitOfWork">
///     The unit of work used to persist the conversation changes.
/// </param>
public class StageUpdatedEventHandler(
    IConversationRepository conversationRepository,
    IUnitOfWork unitOfWork) : IDomainEventHandler<StageUpdatedEvent>
{
    /// <inheritdoc />
    public async Task HandleAsync(StageUpdatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var conversation =
            await conversationRepository.FindByOrderIdAsync(domainEvent.SalesOrderId, cancellationToken);

        // No tracking conversation exists yet for this order; nothing to sync.
        if (conversation is null) return;

        var stage = MapToProductionStage(domainEvent.StageName, domainEvent.ProgressPercent);
        conversation.UpdateProgress(stage, domainEvent.ProgressPercent);

        conversationRepository.Update(conversation);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    /// <summary>
    ///     Maps a manufacturing stage name to the tracking <see cref="EProductionStage" />.
    /// </summary>
    /// <remarks>
    ///     Carpenters may name their stages freely, so we first try to match the canonical
    ///     workflow terms and otherwise fall back to a coarse stage derived from the overall
    ///     progress.
    /// </remarks>
    /// <param name="stageName">The name of the manufacturing stage.</param>
    /// <param name="progressPercent">The overall completion of the order.</param>
    /// <returns>The production stage to display in the tracking conversation.</returns>
    private static EProductionStage MapToProductionStage(string stageName, int progressPercent)
    {
        if (Enum.TryParse<EProductionStage>(stageName, ignoreCase: true, out var stage))
            return stage;

        return progressPercent switch
        {
            <= 0 => EProductionStage.NotStarted,
            >= 100 => EProductionStage.Completed,
            _ => EProductionStage.Assembly
        };
    }
}
