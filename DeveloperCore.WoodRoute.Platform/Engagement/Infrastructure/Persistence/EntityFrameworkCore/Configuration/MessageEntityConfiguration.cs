using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     EF Core configuration for the <see cref="Message" /> entity.
/// </summary>
public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.ConversationId).IsRequired();

        builder.Property(m => m.Content)
            .IsRequired()
            .HasMaxLength(2000); // Reasonable max to avoid very large payloads

        builder.Property(m => m.SenderType)
            .IsRequired()
            .HasMaxLength(20); // "Client" or "Workshop"

        builder.Property(m => m.SenderId).IsRequired();

        builder.Property(m => m.SentAt).IsRequired();

        // Index on ConversationId + SentAt speeds up the paginated history query
        builder.HasIndex(m => new { m.ConversationId, m.SentAt });
    }
}
