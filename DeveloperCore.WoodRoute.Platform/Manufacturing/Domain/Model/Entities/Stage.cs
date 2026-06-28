using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;

/// <summary>
///     Represents a single production stage within a manufacture order.
///     Examples: Diseno, Corte, Ensamblado, Acabado, Entrega.
/// </summary>
/// <remarks>
///     Stages always belong to a ManufactureOrder and are created through it.
///     Status transitions are validated in the aggregate to prevent invalid changes.
/// </remarks>
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

    /// <summary>
    ///     Position of this stage in the overall production sequence.
    ///     Useful for displaying stages in order on the frontend.
    /// </summary>
    public int OrderIndex { get; private set; }

    public EStageStatus Status { get; private set; }

    public DateTimeOffset? StartedAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }

    // IAuditableEntity
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    ///     Updates the status of this stage.
    ///     Only forward transitions are allowed: Pending -> InProgress -> Completed.
    /// </summary>
    public bool TryUpdateStatus(EStageStatus newStatus)
    {
        // Prevent backwards transitions or same-status updates
        if (newStatus <= Status) return false;

        Status = newStatus;

        if (newStatus == EStageStatus.InProgress)
            StartedAt = DateTimeOffset.UtcNow;
        else if (newStatus == EStageStatus.Completed)
            CompletedAt = DateTimeOffset.UtcNow;

        return true;
    }
}
