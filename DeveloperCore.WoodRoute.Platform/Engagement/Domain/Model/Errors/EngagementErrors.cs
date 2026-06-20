using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Errors;

/// <summary>
///     Domain errors for the Engagement bounded context.
/// </summary>
public static class EngagementErrors
{
    public static readonly Error ConversationNotFound =
        new("Engagement.ConversationNotFound", "No conversation was found for the specified order.");

    public static readonly Error MessageContentEmpty =
        new("Engagement.MessageContentEmpty", "The message content cannot be empty.");

    public static readonly Error TrackingIdNotFound =
        new("Engagement.TrackingIdNotFound",
            "No order was found with the specified tracking id. It may be invalid or the order may have been cancelled.");
}
