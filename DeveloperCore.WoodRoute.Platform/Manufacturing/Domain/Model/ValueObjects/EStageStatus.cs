namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     Represents the current status of a production stage.
///     The flow is always: Pending -> InProgress -> Completed.
///     We don't allow going backwards to keep the audit trail consistent.
/// </summary>
public enum EStageStatus
{
    Pending,
    InProgress,
    Completed
}
