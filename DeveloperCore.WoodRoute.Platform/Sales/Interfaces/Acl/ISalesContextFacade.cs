namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Acl;

/// <summary>
///     Anti-corruption facade exposing Sales order state to other bounded contexts.
/// </summary>
public interface ISalesContextFacade
{
    /// <summary>
    ///     Returns whether the order exists and has been accepted by the carpenter.
    /// </summary>
    /// <param name="orderId">
    ///     The identifier of the order to check.
    /// </param>
    /// <returns>
    ///     True if the order exists and has been accepted, otherwise false.
    /// </returns>
    Task<bool> IsOrderAcceptedAsync(int orderId);
}
