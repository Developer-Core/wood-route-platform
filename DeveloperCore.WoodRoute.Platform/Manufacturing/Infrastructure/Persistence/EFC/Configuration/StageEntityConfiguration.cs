using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     EF Core configuration for the <see cref="Stage" /> entity.
/// </summary>
public class StageEntityConfiguration : IEntityTypeConfiguration<Stage>
{
    public void Configure(EntityTypeBuilder<Stage> builder)
    {
        builder.ToTable("stages");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Property(s => s.ManufactureOrderId).IsRequired();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.EstimatedTimeInDays).IsRequired();
        builder.Property(s => s.OrderIndex).IsRequired();

        // Store the status as a readable string instead of a numeric enum value
        builder.Property(s => s.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(s => s.StartedAt);
        builder.Property(s => s.CompletedAt);

        // Index to speed up queries that filter stages by order
        builder.HasIndex(s => s.ManufactureOrderId);
    }
}
