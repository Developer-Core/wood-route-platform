using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Events;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Events;

/// <summary>
///     Domain event raised when a production stage changes its status.
/// </summary>
/// <param name="ManufactureOrderId">Id of the manufacture order the stage belongs to.</param>
/// <param name="SalesOrderId">
///     Id of the originating sales order, used to locate the tracking conversation in the
///     Engagement context.
/// </param>
/// <param name="StageId">Id of the stage whose status changed.</param>
/// <param name="StageName">Name of the stage whose status changed.</param>
/// <param name="NewStatus">The new status of the stage.</param>
/// <param name="ProgressPercent">
///     Overall completion of the manufacture order after this change, in the range [0, 100].
/// </param>
/// <param name="UpdatedByUserId">Id of the user that triggered the status change.</param>
/// <param name="OccurredOn">UTC timestamp when the event occurred.</param>
public record StageUpdatedEvent(
    int ManufactureOrderId,
    int SalesOrderId,
    int StageId,
    string StageName,
    string NewStatus,
    int ProgressPercent,
    int UpdatedByUserId,
    DateTimeOffset OccurredOn
) : IEvent;
