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
    Task<IEnumerable<InventoryMaterial>> Handle(GetAllInventoryMaterialsQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get inventory material by id query.
    /// </summary>
    Task<InventoryMaterial?> Handle(GetInventoryMaterialByIdQuery query,
        CancellationToken cancellationToken = default);
}
