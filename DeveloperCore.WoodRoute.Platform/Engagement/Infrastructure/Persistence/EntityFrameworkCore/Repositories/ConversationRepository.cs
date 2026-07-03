using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     EF Core implementation of <see cref="IConversationRepository" />.
/// </summary>
public class ConversationRepository(AppDbContext context)
    : BaseRepository<Conversation>(context), IConversationRepository
{
    /// <inheritdoc />
    public async Task<Conversation?> FindByPublicTrackingIdAsync(Guid publicTrackingId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Conversation>()
            .FirstOrDefaultAsync(c => c.PublicTrackingId == publicTrackingId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Conversation?> FindByOrderIdAsync(int orderId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Conversation>()
            .FirstOrDefaultAsync(c => c.OrderId == orderId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Message>> GetMessagesByConversationAsync(
        int conversationId,
        int limit,
        DateTimeOffset? before,
        CancellationToken cancellationToken = default)
    {
        var query = Context.Set<Message>()
            .Where(m => m.ConversationId == conversationId);

        // Apply cursor: only return messages older than the given timestamp
        if (before.HasValue)
            query = query.Where(m => m.SentAt < before.Value);

        // Return in descending order so the client gets the most recent ones first
        return await query
            .OrderByDescending(m => m.SentAt)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }
}
