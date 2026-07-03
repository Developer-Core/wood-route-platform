using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Inventory material repository implementation.
/// </summary>
/// <param name="context">The database context</param>
public class InventoryMaterialRepository(AppDbContext context)
    : BaseRepository<InventoryMaterial>(context), IInventoryMaterialRepository;
