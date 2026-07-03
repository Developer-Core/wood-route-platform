namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Acl;

/// <summary>
///     Anti-corruption facade exposing Sales order state to other bounded contexts.
/// </summary>
public interface ISalesContextFacade
{
    /// <summary>
    ///     Returns whether the order exists and has been accepted by the carpenter.
    /// </summary>
    Task<bool> IsOrderAcceptedAsync(int orderId);
}
