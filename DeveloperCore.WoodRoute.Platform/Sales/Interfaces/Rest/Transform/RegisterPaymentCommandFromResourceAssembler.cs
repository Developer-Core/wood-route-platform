using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="RegisterPaymentResource" /> into a <see cref="RegisterPaymentCommand" />.
/// </summary>
public static class RegisterPaymentCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a register payment resource and the target order id to its command representation.
    /// </summary>
    public static RegisterPaymentCommand ToCommandFromResource(int orderId, RegisterPaymentResource resource)
    {
        return new RegisterPaymentCommand(
            orderId,
            resource.Type,
            resource.Amount,
            resource.ReceiptReference);
    }
}
