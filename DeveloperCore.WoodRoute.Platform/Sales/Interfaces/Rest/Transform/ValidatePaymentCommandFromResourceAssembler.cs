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
    /// <param name="orderId">
    ///     The identifier of the order the payment belongs to.
    /// </param>
    /// <param name="paymentId">
    ///     The identifier of the payment to validate.
    /// </param>
    /// <param name="resource">
    ///     The <see cref="ValidatePaymentResource" /> containing the data for validating the payment.
    /// </param>
    /// <returns>
    ///     A new <see cref="ValidatePaymentCommand" /> instance.
    /// </returns>
    public static ValidatePaymentCommand ToCommandFromResource(int orderId, int paymentId,
        ValidatePaymentResource resource)
    {
        return new ValidatePaymentCommand(orderId, paymentId, resource.IsApproved);
    }
}
