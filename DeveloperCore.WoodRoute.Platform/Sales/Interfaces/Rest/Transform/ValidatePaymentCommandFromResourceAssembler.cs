using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="ValidatePaymentResource" /> into a <see cref="ValidatePaymentCommand" />.
/// </summary>
public static class ValidatePaymentCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a validate payment resource and the target order and payment ids to its command representation.
    /// </summary>
    public static ValidatePaymentCommand ToCommandFromResource(int orderId, int paymentId,
        ValidatePaymentResource resource)
    {
        return new ValidatePaymentCommand(orderId, paymentId, resource.IsApproved);
    }
}
