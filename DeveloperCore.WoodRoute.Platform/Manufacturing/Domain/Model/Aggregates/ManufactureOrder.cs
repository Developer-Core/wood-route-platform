using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Events;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;

/// <summary>
///     Aggregate root for the manufacturing process of an order.
///     Groups all the production stages and guards the business rules
///     around defining and updating them.
/// </summary>
/// <remarks>
///     The <see cref="SalesOrderId" /> is a reference to the Order in the Sales context.
///     We don't hold a navigation property to avoid coupling between bounded contexts —
///     just the foreign key integer is enough here.
/// </remarks>
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
    ///     Defines the production stages for this order.
    ///     Can only be called once — stages cannot be redefined after they are set.
    /// </summary>
    /// <param name="stageDefinitions">
    ///     List of (name, estimatedTimeInDays) pairs in the desired execution order.
    /// </param>
    public bool DefineStages(IEnumerable<(string Name, int EstimatedTimeInDays)> stageDefinitions)
    {
        if (StagesAreDefined) return false;

        var index = 0;
        foreach (var (name, days) in stageDefinitions)
        {
            _stages.Add(new Stage(Id, name, days, index));
            index++;
        }

        StagesAreDefined = true;
        return true;
    }

    /// <summary>
    ///     Updates the status of a specific stage and raises <see cref="StageUpdatedDomainEvent" />.
    ///     Returns false if the stage is not found or the transition is invalid.
    /// </summary>
    public bool UpdateStageStatus(int stageId, EStageStatus newStatus, int updatedByUserId)
    {
        var stage = _stages.FirstOrDefault(s => s.Id == stageId);
        if (stage is null) return false;

        var updated = stage.TryUpdateStatus(newStatus);
        if (!updated) return false;

        // Raise the event so Engagement can inform the client about progress
        RaiseDomainEvent(new StageUpdatedDomainEvent(
            Id, stageId, stage.Name, newStatus.ToString(), updatedByUserId, DateTimeOffset.UtcNow));

        return true;
    }
}
