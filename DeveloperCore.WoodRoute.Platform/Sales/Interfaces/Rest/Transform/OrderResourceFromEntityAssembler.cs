using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform an <see cref="Order" /> aggregate into an <see cref="OrderResource" />.
/// </summary>
public static class OrderResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts an order aggregate to its resource representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="Order" /> aggregate to convert.
    /// </param>
    /// <returns>
    ///     A new <see cref="OrderResource" /> instance.
    /// </returns>
    public static OrderResource ToResourceFromEntity(Order entity, (int Completed, int Total) progress = default)
    {
        return new OrderResource(
            entity.Id,
            entity.PublicTrackingId,
            entity.CustomerId,
            entity.CarpenterId,
            entity.Status.ToString(),
            new FurnitureDetailsResource(
                entity.Details.FurnitureType,
                entity.Details.Width,
                entity.Details.Height,
                entity.Details.Depth,
                entity.Details.Material,
                entity.Details.DesignNotes),
            entity.Quote is not null ? QuoteResourceFromEntityAssembler.ToResourceFromEntity(entity.Quote) : null,
            entity.Payments.Select(PaymentResourceFromEntityAssembler.ToResourceFromEntity),
            progress.Completed,
            progress.Total);
    }
}
