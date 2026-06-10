using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Entities;

/// <summary>
///     Quote child entity of the Order aggregate.
/// </summary>
/// <remarks>
///     Represents the quote proposed by the carpenter for an order.
///     It belongs to the Order aggregate boundary and is only created through the aggregate root.
/// </remarks>
public class Quote : IAuditableEntity
{
    private Quote()
    {
    }

    internal Quote(decimal materialsCost, decimal laborCost, int estimatedProductionDays)
    {
        MaterialsCost = materialsCost;
        LaborCost = laborCost;
        EstimatedProductionDays = estimatedProductionDays;
        Status = EQuoteStatus.Proposed;
    }

    public int Id { get; }
    public int OrderId { get; private set; }
    public decimal MaterialsCost { get; private set; }
    public decimal LaborCost { get; private set; }
    public int EstimatedProductionDays { get; private set; }
    public EQuoteStatus Status { get; private set; }

    /// <summary>
    ///     Total cost of the quote, computed from the materials and labor costs.
    /// </summary>
    public decimal Total => MaterialsCost + LaborCost;

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    ///     Accepts the quote.
    /// </summary>
    public Error Accept()
    {
        if (Status is EQuoteStatus.Accepted) return SalesErrors.QuoteAlreadyAccepted;
        if (Status is not EQuoteStatus.Proposed) return SalesErrors.QuoteAlreadyDecided;
        Status = EQuoteStatus.Accepted;
        return Error.None;
    }

    /// <summary>
    ///     Rejects the quote.
    /// </summary>
    public Error Reject()
    {
        if (Status is not EQuoteStatus.Proposed) return SalesErrors.QuoteAlreadyDecided;
        Status = EQuoteStatus.Rejected;
        return Error.None;
    }
}
