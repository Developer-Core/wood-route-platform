using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Inventory bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Inventory bounded context persistence configuration.
    /// </summary>
    public static void ApplyInventoryConfiguration(this ModelBuilder builder)
    {
        // Inventory Context

        // Inventory material aggregate root
        builder.Entity<InventoryMaterial>().HasKey(m => m.Id);
        builder.Entity<InventoryMaterial>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<InventoryMaterial>().Property(m => m.MaterialType).HasMaxLength(100).IsRequired();
        builder.Entity<InventoryMaterial>().Property(m => m.Quantity).HasPrecision(12, 2).IsRequired();
        builder.Entity<InventoryMaterial>().Property(m => m.Unit).HasMaxLength(20).IsRequired();
        builder.Entity<InventoryMaterial>().Property(m => m.MinStock).HasPrecision(12, 2).IsRequired();

        // Domain events are dispatched in memory and never persisted
        builder.Entity<InventoryMaterial>().Ignore(m => m.DomainEvents);
    }
}
