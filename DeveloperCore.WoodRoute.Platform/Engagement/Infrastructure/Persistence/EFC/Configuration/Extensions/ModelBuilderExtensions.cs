using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Engagement.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Engagement bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Engagement bounded context persistence configuration.
    /// </summary>
    public static void ApplyEngagementConfiguration(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ConversationEntityConfiguration());
        builder.ApplyConfiguration(new MessageEntityConfiguration());

        // Messages are always accessed through the Conversation aggregate,
        // so we load the backing field directly and auto-include them when
        // loading a Conversation.
        builder.Entity<Conversation>()
            .Navigation(c => c.Messages)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
