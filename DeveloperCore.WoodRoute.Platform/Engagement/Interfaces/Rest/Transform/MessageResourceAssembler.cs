using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to map between Message entities and REST resources.
/// </summary>
public static class MessageResourceAssembler
{
    /// <summary>Builds a <see cref="SendMessageCommand" /> from a REST request resource.</summary>
    public static SendMessageCommand ToCommandFromResource(int orderId, SendMessageResource resource)
    {
        return new SendMessageCommand(orderId, resource.Content, resource.SenderType, resource.SenderId);
    }

    /// <summary>Builds a <see cref="MessageResource" /> from a domain <see cref="Message" /> entity.</summary>
    public static MessageResource ToResourceFromEntity(Message message)
    {
        return new MessageResource(message.Id, message.Content, message.SenderType, message.SenderId, message.SentAt);
    }
}
