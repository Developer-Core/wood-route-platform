using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Events;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Events;

/// <summary>
///     Domain event raised when a production stage changes its status.
/// </summary>
public record StageUpdatedEvent(
    int ManufactureOrderId,
    int StageId,
    string StageName,
    string NewStatus,
    int UpdatedByUserId,
    DateTimeOffset OccurredOn
) : IEvent;
