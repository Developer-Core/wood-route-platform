using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     EF Core configuration for the <see cref="Conversation" /> aggregate.
/// </summary>
public class ConversationEntityConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.ToTable("conversations");

        builder.HasKey(c => c.Id);

        // The public tracking id must be unique so two orders never share the same public link
        builder.Property(c => c.PublicTrackingId).IsRequired();
        builder.HasIndex(c => c.PublicTrackingId).IsUnique();

        builder.Property(c => c.OrderId).IsRequired();
        builder.HasIndex(c => c.OrderId).IsUnique(); // one conversation per order

        builder.Property(c => c.CurrentStage)
            .HasConversion<string>() // store as string so the value is readable in the database
            .IsRequired();

        builder.Property(c => c.ProgressPercent).IsRequired();

        builder.Property(c => c.EstimatedDeliveryDate);

        // Navigation to messages — EF resolves the back-reference via ConversationId on Message
        builder.HasMany(c => c.Messages)
            .WithOne()
            .HasForeignKey(m => m.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
