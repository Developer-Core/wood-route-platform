using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Domain.Repositories;

/// <summary>
/// Repository contract for inventory materials.
/// </summary>
public interface IInventoryMaterialRepository : IBaseRepository<InventoryMaterial>
{
}