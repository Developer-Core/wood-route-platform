using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;

/// <summary>
///     A single production stage within a manufacture order.
/// </summary>
public class Stage : IAuditableEntity
{
    private Stage()
    {
        Name = string.Empty;
    }

    public Stage(int manufactureOrderId, string name, int estimatedTimeInDays, int orderIndex)
    {
        ManufactureOrderId = manufactureOrderId;
        Name = name;
        EstimatedTimeInDays = estimatedTimeInDays;
        OrderIndex = orderIndex;
        Status = EStageStatus.Pending;
    }

    public int Id { get; private set; }
    public int ManufactureOrderId { get; private set; }

    /// <summary>Name of the stage, e.g. "Corte", "Acabado".</summary>
    public string Name { get; private set; }

    /// <summary>Estimated number of working days this stage will take.</summary>
    public int EstimatedTimeInDays { get; private set; }

    /// <summary>Position of this stage in the production sequence (0-based).</summary>
    public int OrderIndex { get; private set; }

    public EStageStatus Status { get; private set; }

    public DateTimeOffset? StartedAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }

    // IAuditableEntity
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    ///     Changes the status following the allowed flow Pending -> InProgress -> Completed.
    /// </summary>
    public Error ChangeStatus(EStageStatus newStatus)
    {
        switch (newStatus)
        {
            case EStageStatus.InProgress:
                if (Status is not EStageStatus.Pending) return ManufacturingErrors.InvalidStageTransition;
                Status = EStageStatus.InProgress;
                StartedAt = DateTimeOffset.UtcNow;
                return Error.None;
            case EStageStatus.Completed:
                if (Status is not EStageStatus.InProgress) return ManufacturingErrors.InvalidStageTransition;
                Status = EStageStatus.Completed;
                CompletedAt = DateTimeOffset.UtcNow;
                return Error.None;
            default:
                return ManufacturingErrors.InvalidStageTransition;
        }
    }
}
