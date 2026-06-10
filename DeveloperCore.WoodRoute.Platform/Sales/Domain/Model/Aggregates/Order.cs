using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Aggregates;

/// <summary>
///     Order Aggregate Root
/// </summary>
/// <remarks>
///     Represents a custom furniture order placed by a customer to a carpenter.
///     It owns the full order lifecycle, the quote proposed by the carpenter and the
///     payments registered for the order. State transitions are guarded and return a
///     domain <see cref="Error" /> (<see cref="Error.None" /> on success).
/// </remarks>
public class Order : IAuditableEntity
{
    private readonly List<Payment> _payments = [];

    private Order()
    {
        Details = null!;
    }

    public Order(CreateOrderCommand command)
    {
        CustomerId = command.CustomerId;
        CarpenterId = command.CarpenterId;
        Details = new FurnitureDetails(command.FurnitureType, command.Width, command.Height, command.Depth,
            command.Material, command.DesignNotes);
        Status = EOrderStatus.Pending;
        PublicTrackingId = Guid.NewGuid();
    }

    public int Id { get; }
    public int CustomerId { get; private set; }
    public int CarpenterId { get; private set; }
    public FurnitureDetails Details { get; private set; }
    public EOrderStatus Status { get; private set; }

    /// <summary>
    ///     Public tracking id generated at creation, used for unauthenticated order tracking.
    /// </summary>
    public Guid PublicTrackingId { get; private set; }

    public Quote? Quote { get; private set; }
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    ///     Accepts the order. Only pending orders can be accepted.
    /// </summary>
    public Error Accept()
    {
        if (Status is not EOrderStatus.Pending)
            return SalesErrors.InvalidOrderStatusTransition(Status, EOrderStatus.Accepted);
        Status = EOrderStatus.Accepted;
        return Error.None;
    }

    /// <summary>
    ///     Rejects the order. Only pending orders can be rejected.
    /// </summary>
    public Error Reject()
    {
        if (Status is not EOrderStatus.Pending)
            return SalesErrors.InvalidOrderStatusTransition(Status, EOrderStatus.Rejected);
        Status = EOrderStatus.Rejected;
        return Error.None;
    }

    /// <summary>
    ///     Cancels the order. Only pending orders can be cancelled.
    /// </summary>
    public Error Cancel()
    {
        if (Status is not EOrderStatus.Pending)
            return SalesErrors.InvalidOrderStatusTransition(Status, EOrderStatus.Cancelled);
        Status = EOrderStatus.Cancelled;
        return Error.None;
    }

    /// <summary>
    ///     Modifies the furniture details of the order. Only pending orders can be modified.
    /// </summary>
    public Error Modify(ModifyOrderCommand command)
    {
        if (Status is not EOrderStatus.Pending) return SalesErrors.OrderNotPending;
        Details = new FurnitureDetails(command.FurnitureType, command.Width, command.Height, command.Depth,
            command.Material, command.DesignNotes);
        return Error.None;
    }

    /// <summary>
    ///     Marks the order as in progress. Only accepted orders can start production.
    /// </summary>
    public Error MarkInProgress()
    {
        if (Status is not EOrderStatus.Accepted)
            return SalesErrors.InvalidOrderStatusTransition(Status, EOrderStatus.InProgress);
        Status = EOrderStatus.InProgress;
        return Error.None;
    }

    /// <summary>
    ///     Marks the order as ready for delivery. Only in-progress orders can be marked.
    /// </summary>
    public Error MarkReadyForDelivery()
    {
        if (Status is not EOrderStatus.InProgress)
            return SalesErrors.InvalidOrderStatusTransition(Status, EOrderStatus.ReadyForDelivery);
        Status = EOrderStatus.ReadyForDelivery;
        return Error.None;
    }

    /// <summary>
    ///     Completes the order. Only orders ready for delivery can be completed.
    /// </summary>
    public Error Complete()
    {
        if (Status is not EOrderStatus.ReadyForDelivery)
            return SalesErrors.InvalidOrderStatusTransition(Status, EOrderStatus.Completed);
        Status = EOrderStatus.Completed;
        return Error.None;
    }

    /// <summary>
    ///     Generates the quote for the order. Only pending orders without a quote can be quoted.
    /// </summary>
    public Error GenerateQuote(GenerateQuoteCommand command)
    {
        if (Status is not EOrderStatus.Pending) return SalesErrors.OrderNotPending;
        if (Quote is not null) return SalesErrors.QuoteAlreadyExists;
        if (command.MaterialsCost < 0 || command.LaborCost < 0 ||
            command.MaterialsCost + command.LaborCost <= 0)
            return SalesErrors.InvalidQuoteCosts;
        if (command.EstimatedProductionDays <= 0) return SalesErrors.InvalidQuoteProductionDays;
        Quote = new Quote(command.MaterialsCost, command.LaborCost, command.EstimatedProductionDays);
        return Error.None;
    }

    /// <summary>
    ///     Accepts the quote proposed for the order.
    /// </summary>
    public Error AcceptQuote()
    {
        return Quote is null ? SalesErrors.QuoteNotFound : Quote.Accept();
    }

    /// <summary>
    ///     Rejects the quote proposed for the order.
    /// </summary>
    public Error RejectQuote()
    {
        return Quote is null ? SalesErrors.QuoteNotFound : Quote.Reject();
    }

    /// <summary>
    ///     Registers a payment for the order, pending receipt validation.
    /// </summary>
    public Error RegisterPayment(RegisterPaymentCommand command)
    {
        if (Status is not (EOrderStatus.Accepted or EOrderStatus.InProgress or EOrderStatus.ReadyForDelivery))
            return SalesErrors.OrderNotPayable;
        if (command.Amount <= 0) return SalesErrors.InvalidPaymentAmount;
        if (string.IsNullOrWhiteSpace(command.ReceiptReference))
            return SalesErrors.PaymentReceiptReferenceRequired;
        _payments.Add(new Payment(command.Type, command.Amount, command.ReceiptReference));
        return Error.None;
    }

    /// <summary>
    ///     Confirms a payment of the order after validating its receipt.
    /// </summary>
    public Error ConfirmPayment(int paymentId)
    {
        var payment = _payments.FirstOrDefault(payment => payment.Id == paymentId);
        return payment is null ? SalesErrors.PaymentNotFound : payment.Confirm();
    }

    /// <summary>
    ///     Rejects a payment of the order after validating its receipt.
    /// </summary>
    public Error RejectPayment(int paymentId)
    {
        var payment = _payments.FirstOrDefault(payment => payment.Id == paymentId);
        return payment is null ? SalesErrors.PaymentNotFound : payment.Reject();
    }
}
