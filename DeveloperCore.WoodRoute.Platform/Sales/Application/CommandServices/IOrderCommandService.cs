using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;

namespace DeveloperCore.WoodRoute.Platform.Sales.Application.CommandServices;

/// <summary>
///     Order command service contract.
/// </summary>
public interface IOrderCommandService
{
    /// <summary>
    ///     Handles the create order command.
    /// </summary>
    Task<Result<Order>> Handle(CreateOrderCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the modify order command.
    /// </summary>
    Task<Result<Order>> Handle(ModifyOrderCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the cancel order command.
    /// </summary>
    Task<Result<Order>> Handle(CancelOrderCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the accept order command.
    /// </summary>
    Task<Result<Order>> Handle(AcceptOrderCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the reject order command.
    /// </summary>
    Task<Result<Order>> Handle(RejectOrderCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the generate quote command.
    /// </summary>
    Task<Result<Order>> Handle(GenerateQuoteCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the accept quote command.
    /// </summary>
    Task<Result<Order>> Handle(AcceptQuoteCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the register payment command.
    /// </summary>
    Task<Result<Order>> Handle(RegisterPaymentCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the validate payment command.
    /// </summary>
    Task<Result<Order>> Handle(ValidatePaymentCommand command, CancellationToken cancellationToken = default);
}
