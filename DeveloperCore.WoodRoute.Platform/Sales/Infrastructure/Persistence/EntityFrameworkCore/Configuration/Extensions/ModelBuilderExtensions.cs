using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Sales bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Sales bounded context persistence configuration.
    /// </summary>
    public static void ApplySalesConfiguration(this ModelBuilder builder)
    {
        // Sales Context

        // Order aggregate root
        builder.Entity<Order>().HasKey(o => o.Id);
        builder.Entity<Order>().Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Order>().Property(o => o.CustomerId).IsRequired();
        builder.Entity<Order>().Property(o => o.CarpenterId).IsRequired();
        builder.Entity<Order>().Property(o => o.Status).HasConversion<string>().HasMaxLength(40).IsRequired();
        builder.Entity<Order>().Property(o => o.PublicTrackingId).IsRequired();
        builder.Entity<Order>().HasIndex(o => o.PublicTrackingId).IsUnique();

        // Furniture details owned value object, stored in the orders table
        builder.Entity<Order>().OwnsOne(o => o.Details,
            details =>
            {
                details.WithOwner();
                details.Property(d => d.FurnitureType).HasColumnName("FurnitureType").HasMaxLength(100).IsRequired();
                details.Property(d => d.Width).HasColumnName("Width").HasPrecision(8, 2).IsRequired();
                details.Property(d => d.Height).HasColumnName("Height").HasPrecision(8, 2).IsRequired();
                details.Property(d => d.Depth).HasColumnName("Depth").HasPrecision(8, 2).IsRequired();
                details.Property(d => d.Material).HasColumnName("Material").HasMaxLength(100).IsRequired();
                details.Property(d => d.DesignNotes).HasColumnName("DesignNotes").HasMaxLength(1000).IsRequired();
            });

        // Quote child entity
        builder.Entity<Quote>().HasKey(q => q.Id);
        builder.Entity<Quote>().Property(q => q.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Quote>().Property(q => q.MaterialsCost).HasPrecision(12, 2).IsRequired();
        builder.Entity<Quote>().Property(q => q.LaborCost).HasPrecision(12, 2).IsRequired();
        builder.Entity<Quote>().Property(q => q.EstimatedProductionDays).IsRequired();
        builder.Entity<Quote>().Property(q => q.Status).HasConversion<string>().HasMaxLength(40).IsRequired();

        // Payment child entity
        builder.Entity<Payment>().HasKey(p => p.Id);
        builder.Entity<Payment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Payment>().Property(p => p.Type).HasConversion<string>().HasMaxLength(40).IsRequired();
        builder.Entity<Payment>().Property(p => p.Amount).HasPrecision(12, 2).IsRequired();
        builder.Entity<Payment>().Property(p => p.ReceiptReference).HasMaxLength(200).IsRequired();
        builder.Entity<Payment>().Property(p => p.Status).HasConversion<string>().HasMaxLength(40).IsRequired();

        // Aggregate relationships
        builder.Entity<Order>()
            .HasOne(o => o.Quote)
            .WithOne()
            .HasForeignKey<Quote>(q => q.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Order>()
            .HasMany(o => o.Payments)
            .WithOne()
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Aggregates are always loaded whole: access payments through the backing
        // field and auto-include the child entities in every order query.
        builder.Entity<Order>().Navigation(o => o.Payments)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Entity<Order>().Navigation(o => o.Quote).AutoInclude();
        builder.Entity<Order>().Navigation(o => o.Payments).AutoInclude();
    }
}
