namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Acl;

/// <summary>
///     Anti-corruption facade exposing Manufacturing production progress to other bounded contexts.
/// </summary>
public interface IManufacturingContextFacade
{
    /// <summary>
    ///     Returns the production progress of the order linked to the given sales order id, as the
    ///     number of completed stages over the total. Orders without a production plan report (0, 0).
    /// </summary>
    /// <param name="salesOrderId">The identifier of the sales order.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A tuple with the number of completed stages and the total number of stages.</returns>
    Task<(int Completed, int Total)> GetStageProgressAsync(int salesOrderId,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns the production progress for many sales orders in a single query, avoiding N+1.
    ///     Sales orders without a production plan are absent from the map; consumers assume (0, 0).
    /// </summary>
    /// <param name="salesOrderIds">The identifiers of the sales orders.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A map from sales order id to its (completed, total) stage progress.</returns>
    Task<IReadOnlyDictionary<int, (int Completed, int Total)>> GetStageProgressForOrdersAsync(
        IEnumerable<int> salesOrderIds, CancellationToken cancellationToken = default);
}
