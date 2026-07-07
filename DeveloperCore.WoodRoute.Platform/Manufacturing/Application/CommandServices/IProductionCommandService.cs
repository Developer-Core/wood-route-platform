using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Application.CommandServices;

/// <summary>
///     Production command service contract.
/// </summary>
public interface IProductionCommandService
{
    /// <summary>
    ///     Handles the define stages command.
    /// </summary>
    Task<Result<ManufactureOrder>> Handle(DefineStagesCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the update (re-define) stages command.
    /// </summary>
    Task<Result<ManufactureOrder>> Handle(UpdateStagesCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the update stage status command.
    /// </summary>
    Task<Result<Stage>> Handle(UpdateStageStatusCommand command, CancellationToken cancellationToken = default);
}
