using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.ValueObjects;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to update the status of a specific production stage.
/// </summary>
public record UpdateStageStatusCommand(
    int SalesOrderId,
    int StageId,
    EStageStatus NewStatus,
    int RequestingUserId
);
