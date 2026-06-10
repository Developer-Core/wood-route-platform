using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Entities;

/// <summary>
///     Payment child entity of the Order aggregate.
/// </summary>
/// <remarks>
///     Receipts are manually validated by the carpenter, so each payment starts pending validation.
///     It belongs to the Order aggregate boundary and is only created through the aggregate root.
/// </remarks>
public class Payment : IAuditableEntity
{
    private Payment()
    {
        ReceiptReference = string.Empty;
    }

    internal Payment(EPaymentType type, decimal amount, string receiptReference)
    {
        Type = type;
        Amount = amount;
        ReceiptReference = receiptReference;
        Status = EPaymentStatus.PendingValidation;
    }

    public int Id { get; }
    public int OrderId { get; private set; }
    public EPaymentType Type { get; private set; }
    public decimal Amount { get; private set; }
    public string ReceiptReference { get; private set; }
    public EPaymentStatus Status { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    ///     Confirms the payment after validating its receipt.
    /// </summary>
    public Error Confirm()
    {
        if (Status is not EPaymentStatus.PendingValidation) return SalesErrors.PaymentAlreadyValidated;
        Status = EPaymentStatus.Confirmed;
        return Error.None;
    }

    /// <summary>
    ///     Rejects the payment after validating its receipt.
    /// </summary>
    public Error Reject()
    {
        if (Status is not EPaymentStatus.PendingValidation) return SalesErrors.PaymentAlreadyValidated;
        Status = EPaymentStatus.Rejected;
        return Error.None;
    }
}
