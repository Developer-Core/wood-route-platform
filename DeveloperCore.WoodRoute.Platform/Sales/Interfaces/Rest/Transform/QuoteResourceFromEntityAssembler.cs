using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="Quote" /> entity into a <see cref="QuoteResource" />.
/// </summary>
public static class QuoteResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a quote entity to its resource representation.
    /// </summary>
    public static QuoteResource ToResourceFromEntity(Quote entity)
    {
        return new QuoteResource(
            entity.Id,
            entity.MaterialsCost,
            entity.LaborCost,
            entity.Total,
            entity.EstimatedProductionDays,
            entity.Status.ToString());
    }
}
