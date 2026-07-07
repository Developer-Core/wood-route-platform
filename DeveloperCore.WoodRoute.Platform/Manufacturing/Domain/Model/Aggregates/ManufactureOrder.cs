using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Events;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;

/// <summary>
///     Aggregate root for the manufacturing process of a sales order.
/// </summary>
public class ManufactureOrder : AggregateRoot, IAuditableEntity
{
    private readonly List<Stage> _stages = [];

    private ManufactureOrder()
    {
    }

    public ManufactureOrder(int salesOrderId, int carpenterId)
    {
        SalesOrderId = salesOrderId;
        CarpenterId = carpenterId;
        StagesAreDefined = false;
    }

    public int Id { get; private set; }

    /// <summary>
    ///     Reference to the original order in the Sales context.
    ///     We only store the id to avoid a cross-context navigation property.
    /// </summary>
    public int SalesOrderId { get; private set; }

    /// <summary>The carpenter responsible for executing this manufacture order.</summary>
    public int CarpenterId { get; private set; }

    /// <summary>
    ///     Tracks whether the carpenter has already defined the production stages.
    ///     We use this flag to prevent stages from being redefined after the plan is locked in.
    /// </summary>
    public bool StagesAreDefined { get; private set; }

    public IReadOnlyCollection<Stage> Stages => _stages.AsReadOnly();

    // IAuditableEntity
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    ///     Defines the production stages for this order. Can only be called once.
    /// </summary>
    public Error DefineStages(IEnumerable<(string Name, int EstimatedTimeInDays)> stageDefinitions)
    {
        if (StagesAreDefined) return ManufacturingErrors.StagesAlreadyDefined;

        var definitions = stageDefinitions.ToList();
        if (definitions.Count == 0) return ManufacturingErrors.EmptyStageList;

        var index = 0;
        foreach (var (name, days) in definitions)
        {
            _stages.Add(new Stage(Id, name, days, index));
            index++;
        }

        StagesAreDefined = true;
        return Error.None;
    }

    /// <summary>
    ///     Re-defines the production plan while none of the stages have started yet. The whole plan is
    ///     replaced: the previous stages are cleared and the new ones re-created from scratch.
    /// </summary>
    public Error RedefineStages(IEnumerable<(string Name, int EstimatedTimeInDays)> stageDefinitions)
    {
        // Editing is only allowed while the plan is untouched: every stage must still be Pending.
        if (_stages.Any(s => s.Status != EStageStatus.Pending))
            return ManufacturingErrors.StagesAlreadyStarted;

        var definitions = stageDefinitions.ToList();
        if (definitions.Count == 0) return ManufacturingErrors.EmptyStageList;

        _stages.Clear();

        var index = 0;
        foreach (var (name, days) in definitions)
        {
            _stages.Add(new Stage(Id, name, days, index));
            index++;
        }

        StagesAreDefined = true;
        return Error.None;
    }

    /// <summary>
    ///     Updates the status of a specific stage and raises <see cref="StageUpdatedEvent" />.
    /// </summary>
    public Error UpdateStageStatus(int stageId, EStageStatus newStatus, int updatedByUserId)
    {
        var stage = _stages.FirstOrDefault(s => s.Id == stageId);
        if (stage is null) return ManufacturingErrors.StageNotFound;

        var error = stage.ChangeStatus(newStatus);
        if (error != Error.None) return error;

        RaiseDomainEvent(new StageUpdatedEvent(
            Id, SalesOrderId, stageId, stage.Name, newStatus.ToString(),
            CalculateProgressPercent(), updatedByUserId, DateTimeOffset.UtcNow));

        return Error.None;
    }

    /// <summary>
    ///     Calculates the overall completion of the order as the share of completed stages,
    ///     expressed as a whole percentage in the range [0, 100].
    /// </summary>
    private int CalculateProgressPercent()
    {
        if (_stages.Count == 0) return 0;

        var completed = _stages.Count(s => s.Status == EStageStatus.Completed);
        return (int)Math.Round(completed * 100.0 / _stages.Count, MidpointRounding.AwayFromZero);
    }
}
