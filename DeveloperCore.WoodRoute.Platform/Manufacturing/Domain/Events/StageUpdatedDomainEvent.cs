using DeveloperCore.WoodRoute.Platform.Shared.Domain.Events;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Events;

/// <summary>
///     Domain event raised when a production stage changes its status.
///     The Engagement context can listen to this event and notify the client
///     about progress updates on their order.
/// </summary>
/// <param name="ManufactureOrderId">Id of the manufacture order that owns this stage.</param>
/// <param name="StageId">Id of the stage that was updated.</param>
/// <param name="StageName">Human-readable name of the stage (e.g. "Corte", "Acabado").</param>
/// <param name="NewStatus">The new status the stage transitioned to.</param>
/// <param name="UpdatedByUserId">Id of the workshop user who triggered the update.</param>
/// <param name="OccurredOn">UTC timestamp of when the event occurred.</param>
public record StageUpdatedDomainEvent(
    int ManufactureOrderId,
    int StageId,
    string StageName,
    string NewStatus,
    int UpdatedByUserId,
    DateTimeOffset OccurredOn
) : IDomainEvent;
