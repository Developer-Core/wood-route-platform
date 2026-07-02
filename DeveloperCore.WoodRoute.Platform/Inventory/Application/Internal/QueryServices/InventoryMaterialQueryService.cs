using DeveloperCore.WoodRoute.Platform.Inventory.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Application.Internal.QueryServices;

/// <summary>
///     Inventory material query service implementation.
/// </summary>
public class InventoryMaterialQueryService(IInventoryMaterialRepository inventoryMaterialRepository)
    : IInventoryMaterialQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<InventoryMaterial>> Handle(GetAllInventoryMaterialsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await inventoryMaterialRepository.ListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<InventoryMaterial?> Handle(GetInventoryMaterialByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await inventoryMaterialRepository.FindByIdAsync(query.MaterialId, cancellationToken);
    }
}
