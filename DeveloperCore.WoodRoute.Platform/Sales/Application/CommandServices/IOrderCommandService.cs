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
    /// <param name="command">
    ///     The <see cref="CreateOrderCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{Order}" /> with the created order.
    /// </returns>
    Task<Result<Order>> Handle(CreateOrderCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the modify order command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="ModifyOrderCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{Order}" /> with the modified order.
    /// </returns>
    Task<Result<Order>> Handle(ModifyOrderCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the cancel order command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CancelOrderCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{Order}" /> with the cancelled order.
    /// </returns>
    Task<Result<Order>> Handle(CancelOrderCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the accept order command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="AcceptOrderCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{Order}" /> with the accepted order.
    /// </returns>
    Task<Result<Order>> Handle(AcceptOrderCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the reject order command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="RejectOrderCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{Order}" /> with the rejected order.
    /// </returns>
    Task<Result<Order>> Handle(RejectOrderCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the generate quote command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="GenerateQuoteCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{Order}" /> with the quoted order.
    /// </returns>
    Task<Result<Order>> Handle(GenerateQuoteCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the accept quote command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="AcceptQuoteCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{Order}" /> with the order whose quote was accepted.
    /// </returns>
    Task<Result<Order>> Handle(AcceptQuoteCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the register payment command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="RegisterPaymentCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{Order}" /> with the order and the registered payment.
    /// </returns>
    Task<Result<Order>> Handle(RegisterPaymentCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the validate payment command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="ValidatePaymentCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{Order}" /> with the order and the validated payment.
    /// </returns>
    Task<Result<Order>> Handle(ValidatePaymentCommand command, CancellationToken cancellationToken = default);
}
