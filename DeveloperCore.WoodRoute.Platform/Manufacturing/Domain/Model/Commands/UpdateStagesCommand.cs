namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to re-define the ordered production stages of an order while none of them have started.
/// </summary>
public record UpdateStagesCommand(
    int SalesOrderId,
    int CarpenterId,
    IReadOnlyList<StageDefinition> Stages
);
