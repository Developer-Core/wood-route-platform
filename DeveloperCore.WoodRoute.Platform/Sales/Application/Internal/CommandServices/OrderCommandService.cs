using DeveloperCore.WoodRoute.Platform.Sales.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Sales.Application.Internal.CommandServices;

/// <summary>
///     Order command service implementation.
/// </summary>
public class OrderCommandService(IOrderRepository orderRepository, IUnitOfWork unitOfWork) : IOrderCommandService
{
    /// <inheritdoc />
    public async Task<Result<Order>> Handle(CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        var order = new Order(command);
        await orderRepository.AddAsync(order, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Order>.Success(order);
    }

    /// <inheritdoc />
    public Task<Result<Order>> Handle(ModifyOrderCommand command, CancellationToken cancellationToken = default)
    {
        return ApplyToOrderAsync(command.OrderId, order => order.Modify(command), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<Order>> Handle(CancelOrderCommand command, CancellationToken cancellationToken = default)
    {
        return ApplyToOrderAsync(command.OrderId, order => order.Cancel(), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<Order>> Handle(AcceptOrderCommand command, CancellationToken cancellationToken = default)
    {
        return ApplyToOrderAsync(command.OrderId, order => order.Accept(), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<Order>> Handle(RejectOrderCommand command, CancellationToken cancellationToken = default)
    {
        return ApplyToOrderAsync(command.OrderId, order => order.Reject(), cancellationToken);
    }

    /// <summary>
    ///     Loads the order, applies a guarded aggregate behavior and persists the changes on success.
    /// </summary>
    private async Task<Result<Order>> ApplyToOrderAsync(int orderId, Func<Order, Error> behavior,
        CancellationToken cancellationToken)
    {
        var order = await orderRepository.FindByIdAsync(orderId, cancellationToken);
        if (order is null) return Result<Order>.Failure(SalesErrors.OrderNotFound);

        var error = behavior(order);
        if (error != Error.None) return Result<Order>.Failure(error);

        orderRepository.Update(order);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Order>.Success(order);
    }
}
