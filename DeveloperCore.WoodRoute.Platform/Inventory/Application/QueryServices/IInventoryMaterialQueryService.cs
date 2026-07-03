using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Queries;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Application.QueryServices;

/// <summary>
///     Inventory material query service contract.
/// </summary>
public interface IInventoryMaterialQueryService
{
    /// <summary>
    ///     Handles the get all inventory materials query.
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetAllInventoryMaterialsQuery" /> to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A collection of <see cref="InventoryMaterial" /> aggregates.
    /// </returns>
    Task<IEnumerable<InventoryMaterial>> Handle(GetAllInventoryMaterialsQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get inventory material by id query.
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetInventoryMaterialByIdQuery" /> to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The matching <see cref="InventoryMaterial" /> aggregate, or <c>null</c> if not found.
    /// </returns>
    Task<InventoryMaterial?> Handle(GetInventoryMaterialByIdQuery query,
        CancellationToken cancellationToken = default);
}
