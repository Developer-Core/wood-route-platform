namespace DeveloperCore.WoodRoute.Platform.Inventory.Domain.Repositories;

using Model.Aggregates;

public interface IInventoryMaterialRepository
{
    Task<InventoryMaterial?> FindByIdAsync(int id);
    Task<IEnumerable<InventoryMaterial>> ListAsync();
    Task AddAsync(InventoryMaterial material);
    void Update(InventoryMaterial material);
    void Remove(InventoryMaterial material);
}