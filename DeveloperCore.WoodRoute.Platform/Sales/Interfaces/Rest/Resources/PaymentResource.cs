namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Payment of an order for the REST API.
/// </summary>
/// <param name="Id">
///     The identifier of the payment.
/// </param>
/// <param name="Type">
///     The type of the payment.
/// </param>
/// <param name="Amount">
///     The amount of the payment.
/// </param>
/// <param name="ReceiptReference">
///     The reference of the payment receipt.
/// </param>
/// <param name="Status">
///     The current status of the payment.
/// </param>
public record PaymentResource(
    int Id,
    string Type,
    decimal Amount,
    string ReceiptReference,
    string Status);
