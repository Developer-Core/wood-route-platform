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

    /// <summary>
    ///     Resolves the database identifier of the order that owns the given public tracking id.
    /// </summary>
    /// <param name="publicTrackingId">
    ///     The public tracking identifier generated when the order was created.
    /// </param>
    /// <returns>
    ///     The order identifier if an order with the tracking id exists, otherwise <c>null</c>.
    /// </returns>
    Task<int?> GetOrderIdByPublicTrackingIdAsync(Guid publicTrackingId);

    /// <summary>
    ///     Resolves the public tracking id of the order with the given identifier.
    /// </summary>
    /// <param name="orderId">
    ///     The identifier of the order.
    /// </param>
    /// <returns>
    ///     The public tracking id if the order exists, otherwise <c>null</c>.
    /// </returns>
    Task<Guid?> GetPublicTrackingIdByOrderIdAsync(int orderId);
}
