using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="GenerateQuoteResource" /> into a <see cref="GenerateQuoteCommand" />.
/// </summary>
public static class GenerateQuoteCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a generate quote resource and the target order id to its command representation.
    /// </summary>
    /// <param name="orderId">
    ///     The identifier of the order to quote.
    /// </param>
    /// <param name="resource">
    ///     The <see cref="GenerateQuoteResource" /> containing the data for generating the quote.
    /// </param>
    /// <returns>
    ///     A new <see cref="GenerateQuoteCommand" /> instance.
    /// </returns>
    public static GenerateQuoteCommand ToCommandFromResource(int orderId, GenerateQuoteResource resource)
    {
        return new GenerateQuoteCommand(
            orderId,
            resource.MaterialsCost,
            resource.LaborCost,
            resource.EstimatedProductionDays);
    }
}
