namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to define the ordered production stages for an accepted order.
/// </summary>
public record DefineStagesCommand(
    int SalesOrderId,
    int CarpenterId,
    IReadOnlyList<StageDefinition> Stages
);
