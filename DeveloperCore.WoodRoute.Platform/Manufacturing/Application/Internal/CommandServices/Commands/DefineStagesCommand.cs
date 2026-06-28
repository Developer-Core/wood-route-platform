namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.CommandServices.Commands;

/// <summary>
///     Command to define the production stages for an accepted order.
///     Each entry in <see cref="Stages" /> becomes a <c>Stage</c> entity in the aggregate.
/// </summary>
/// <param name="SalesOrderId">Id of the order in the Sales context whose production plan is being defined.</param>
/// <param name="CarpenterId">Id of the carpenter who is defining the plan.</param>
/// <param name="Stages">
///     Ordered list of stages to create. The order of the list defines the execution sequence.
/// </param>
public record DefineStagesCommand(
    int SalesOrderId,
    int CarpenterId,
    IReadOnlyList<StageDefinition> Stages
);

/// <summary>
///     Definition of a single production stage within a <see cref="DefineStagesCommand" />.
/// </summary>
/// <param name="Name">Name of the stage, e.g. "Corte", "Ensamblado".</param>
/// <param name="EstimatedTimeInDays">Estimated working days for this stage.</param>
public record StageDefinition(string Name, int EstimatedTimeInDays);
