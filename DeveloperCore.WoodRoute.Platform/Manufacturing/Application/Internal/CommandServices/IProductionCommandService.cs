using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.CommandServices;

/// <summary>
///     Contract for the production command service.
/// </summary>
public interface IProductionCommandService
{
    /// <summary>
    ///     Handles the define stages command.
    ///     Creates a new ManufactureOrder (if one does not exist yet) and locks in the production plan.
    ///     Returns the full order with its stages on success.
    /// </summary>
    Task<Result<ManufactureOrder>> Handle(DefineStagesCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the update stage status command.
    ///     Returns the updated stage on success.
    ///     Returns a failure result if the stage was not found, the transition is invalid,
    ///     or the requesting user is not the assigned carpenter.
    /// </summary>
    Task<Result<Stage>> Handle(UpdateStageStatusCommand command,
        CancellationToken cancellationToken = default);
}
