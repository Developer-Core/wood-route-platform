using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="Payment" /> entity into a <see cref="PaymentResource" />.
/// </summary>
public static class PaymentResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a payment entity to its resource representation.
    /// </summary>
    public static PaymentResource ToResourceFromEntity(Payment entity)
    {
        return new PaymentResource(
            entity.Id,
            entity.Type.ToString(),
            entity.Amount,
            entity.ReceiptReference,
            entity.Status.ToString());
    }
}
