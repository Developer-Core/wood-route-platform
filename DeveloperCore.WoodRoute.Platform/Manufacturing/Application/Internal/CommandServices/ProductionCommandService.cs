using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.CommandServices.Commands;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.CommandServices;

/// <summary>
///     Handles production commands for manufacture orders.
/// </summary>
/// <remarks>
///     We depend on <see cref="IOrderRepository" /> from the Sales context to verify the order status
///     before allowing stages to be defined. This is an allowed anti-corruption layer query between
///     bounded contexts, since they share the same database in this deployment.
/// </remarks>
public class ProductionCommandService(
    IManufactureOrderRepository manufactureOrderRepository,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : IProductionCommandService
{
    /// <inheritdoc />
    public async Task<Result<ManufactureOrder>> Handle(DefineStagesCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.Stages.Count == 0)
            return Result<ManufactureOrder>.Failure(ManufacturingErrors.EmptyStageList);

        // Verify the sales order exists and is in Accepted status
        var salesOrder = await orderRepository.FindByIdAsync(command.SalesOrderId, cancellationToken);
        if (salesOrder is null)
            return Result<ManufactureOrder>.Failure(ManufacturingErrors.ManufactureOrderNotFound);

        if (salesOrder.Status != EOrderStatus.Accepted)
            return Result<ManufactureOrder>.Failure(ManufacturingErrors.OrderNotAccepted);

        // Find or create the manufacture order for this sales order
        var manufactureOrder =
            await manufactureOrderRepository.FindBySalesOrderIdAsync(command.SalesOrderId, cancellationToken);

        if (manufactureOrder is null)
        {
            manufactureOrder = new ManufactureOrder(command.SalesOrderId, command.CarpenterId);
            await manufactureOrderRepository.AddAsync(manufactureOrder, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
        }

        var definitions = command.Stages.Select(s => (s.Name, s.EstimatedTimeInDays));
        var defined = manufactureOrder.DefineStages(definitions);

        if (!defined)
            return Result<ManufactureOrder>.Failure(ManufacturingErrors.StagesAlreadyDefined);

        manufactureOrderRepository.Update(manufactureOrder);
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

        // Authorization check: only the assigned carpenter can update stages
        if (manufactureOrder.CarpenterId != command.RequestingUserId)
            return Result<Stage>.Failure(ManufacturingErrors.UnauthorizedStageUpdate);

        var updated = manufactureOrder.UpdateStageStatus(command.StageId, command.NewStatus, command.RequestingUserId);

        if (!updated)
        {
            // Could be StageNotFound or InvalidStageTransition — check which one
            var stageExists = manufactureOrder.Stages.Any(s => s.Id == command.StageId);
            return stageExists
                ? Result<Stage>.Failure(ManufacturingErrors.InvalidStageTransition)
                : Result<Stage>.Failure(ManufacturingErrors.StageNotFound);
        }

        manufactureOrderRepository.Update(manufactureOrder);
        await unitOfWork.CompleteAsync(cancellationToken);

        var updatedStage = manufactureOrder.Stages.First(s => s.Id == command.StageId);
        return Result<Stage>.Success(updatedStage);
    }
}
