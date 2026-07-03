using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Acl;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.CommandServices;

/// <summary>
///     Handles production commands for manufacture orders.
/// </summary>
public class ProductionCommandService(
    IManufactureOrderRepository manufactureOrderRepository,
    ISalesContextFacade salesContextFacade,
    IUnitOfWork unitOfWork) : IProductionCommandService
{
    /// <inheritdoc />
    public async Task<Result<ManufactureOrder>> Handle(DefineStagesCommand command,
        CancellationToken cancellationToken = default)
    {
        var isAccepted = await salesContextFacade.IsOrderAcceptedAsync(command.SalesOrderId);
        if (!isAccepted)
            return Result<ManufactureOrder>.Failure(ManufacturingErrors.OrderNotAccepted);

        var manufactureOrder =
            await manufactureOrderRepository.FindBySalesOrderIdAsync(command.SalesOrderId, cancellationToken);
        var isNew = manufactureOrder is null;
        manufactureOrder ??= new ManufactureOrder(command.SalesOrderId, command.CarpenterId);

        var error = manufactureOrder.DefineStages(command.Stages.Select(s => (s.Name, s.EstimatedTimeInDays)));
        if (error != Error.None) return Result<ManufactureOrder>.Failure(error);

        if (isNew) await manufactureOrderRepository.AddAsync(manufactureOrder, cancellationToken);
        else manufactureOrderRepository.Update(manufactureOrder);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<ManufactureOrder>.Success(manufactureOrder);
    }

    /// <inheritdoc />
    public async Task<Result<Stage>> Handle(UpdateStageStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        var manufactureOrder =
            await manufactureOrderRepository.FindBySalesOrderIdAsync(command.SalesOrderId, cancellationToken);

        if (manufactureOrder is null)
            return Result<Stage>.Failure(ManufacturingErrors.ManufactureOrderNotFound);

        if (manufactureOrder.CarpenterId != command.RequestingUserId)
            return Result<Stage>.Failure(ManufacturingErrors.UnauthorizedStageUpdate);

        var error = manufactureOrder.UpdateStageStatus(command.StageId, command.NewStatus, command.RequestingUserId);
        if (error != Error.None) return Result<Stage>.Failure(error);

        manufactureOrderRepository.Update(manufactureOrder);
        await unitOfWork.CompleteAsync(cancellationToken);

        var updatedStage = manufactureOrder.Stages.First(s => s.Id == command.StageId);
        return Result<Stage>.Success(updatedStage);
    }
}
