using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     EF Core configuration for the <see cref="ManufactureOrder" /> aggregate root.
/// </summary>
public class ManufactureOrderEntityConfiguration : IEntityTypeConfiguration<ManufactureOrder>
{
    public void Configure(EntityTypeBuilder<ManufactureOrder> builder)
    {
        builder.ToTable("manufacture_orders");

        builder.HasKey(mo => mo.Id);
        builder.Property(mo => mo.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Property(mo => mo.SalesOrderId).IsRequired();
        // One manufacture order per sales order
        builder.HasIndex(mo => mo.SalesOrderId).IsUnique();

        builder.Property(mo => mo.CarpenterId).IsRequired();
        builder.Property(mo => mo.StagesAreDefined).IsRequired();

        // Stages are owned by this aggregate — cascade delete keeps the DB clean
        builder.HasMany(mo => mo.Stages)
            .WithOne()
            .HasForeignKey(s => s.ManufactureOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Always load stages with the aggregate so business logic has access to them
        builder.Navigation(mo => mo.Stages)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(mo => mo.Stages).AutoInclude();
    }
}
