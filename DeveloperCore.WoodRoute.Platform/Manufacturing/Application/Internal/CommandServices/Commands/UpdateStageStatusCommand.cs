using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.ValueObjects;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.CommandServices.Commands;

/// <summary>
///     Command to update the status of a specific production stage.
///     The carpenter sends this when they start or finish a stage.
/// </summary>
/// <param name="SalesOrderId">Id of the related sales order.</param>
/// <param name="StageId">Id of the stage to update.</param>
/// <param name="NewStatus">The new status to apply.</param>
/// <param name="RequestingUserId">
///     Id of the user making the request. Used to verify that only the
///     assigned carpenter can update stages (authorization check).
/// </param>
public record UpdateStageStatusCommand(
    int SalesOrderId,
    int StageId,
    EStageStatus NewStatus,
    int RequestingUserId
);
