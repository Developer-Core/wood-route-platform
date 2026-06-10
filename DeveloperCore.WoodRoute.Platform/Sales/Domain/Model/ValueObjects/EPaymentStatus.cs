namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;

/// <summary>
///     Represents the validation status of a payment receipt.
/// </summary>
public enum EPaymentStatus
{
    PendingValidation,
    Confirmed,
    Rejected
}
