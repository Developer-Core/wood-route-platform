using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;

namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Errors;

/// <summary>
///     Domain errors for the Sales bounded context.
/// </summary>
public static class SalesErrors
{
    public static readonly Error OrderNotFound =
        new("Sales.OrderNotFound", "The specified order was not found.");

    public static readonly Error OrderNotPending =
        new("Sales.OrderNotPending", "The operation is only allowed while the order is pending.");

    public static readonly Error OrderAlreadyAssigned =
        new("Sales.OrderAlreadyAssigned", "The order is already assigned to a carpenter.");

    public static readonly Error OrderNotAccepted =
        new("Sales.OrderNotAccepted", "The quote can only be generated once the order has been accepted.");

    public static readonly Error QuoteNotFound =
        new("Sales.QuoteNotFound", "The order does not have a quote yet.");

    public static readonly Error QuoteAlreadyExists =
        new("Sales.QuoteAlreadyExists", "The order already has a quote.");

    public static readonly Error QuoteAlreadyAccepted =
        new("Sales.QuoteAlreadyAccepted", "The quote has already been accepted.");

    public static readonly Error QuoteAlreadyDecided =
        new("Sales.QuoteAlreadyDecided", "The quote has already been accepted or rejected.");

    public static readonly Error InvalidQuoteCosts =
        new("Sales.InvalidQuoteCosts", "Quote costs cannot be negative and the total must be positive.");

    public static readonly Error InvalidQuoteProductionDays =
        new("Sales.InvalidQuoteProductionDays", "The estimated production days must be a positive value.");

    public static readonly Error PaymentNotFound =
        new("Sales.PaymentNotFound", "The specified payment was not found in the order.");

    public static readonly Error PaymentAlreadyValidated =
        new("Sales.PaymentAlreadyValidated", "The payment receipt has already been validated.");

    public static readonly Error InvalidPaymentAmount =
        new("Sales.InvalidPaymentAmount", "The payment amount must be a positive value.");

    public static readonly Error PaymentReceiptReferenceRequired =
        new("Sales.PaymentReceiptReferenceRequired", "A receipt reference is required to register a payment.");

    public static readonly Error OrderNotPayable =
        new("Sales.OrderNotPayable", "Payments can only be registered for accepted or in-production orders.");

    /// <summary>
    ///     Builds an error describing an invalid order status transition.
    /// </summary>
    public static Error InvalidOrderStatusTransition(EOrderStatus current, EOrderStatus target)
    {
        return new Error("Sales.InvalidOrderStatusTransition",
            $"The order cannot transition from {current} to {target}.");
    }
}
