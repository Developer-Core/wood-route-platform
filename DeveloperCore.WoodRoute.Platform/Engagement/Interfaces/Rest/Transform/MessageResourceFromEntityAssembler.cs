using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to map between Message entities and REST resources.
/// </summary>
public static class MessageResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds a <see cref="SendMessageCommand" /> from a REST request resource.
    /// </summary>
    /// <param name="orderId">
    ///     Id of the order whose conversation receives the message.
    /// </param>
    /// <param name="resource">
    ///     The <see cref="SendMessageResource" /> request body containing the message data.
    /// </param>
    /// <returns>
    ///     A <see cref="SendMessageCommand" /> populated from the provided order id and resource.
    /// </returns>
    public static SendMessageCommand ToCommandFromResource(int orderId, SendMessageResource resource)
    {
        return new SendMessageCommand(orderId, resource.Content, resource.SenderType, resource.SenderId);
    }

    /// <summary>
    ///     Builds a <see cref="MessageResource" /> from a domain <see cref="Message" /> entity.
    /// </summary>
    /// <param name="message">
    ///     The <see cref="Message" /> entity to convert.
    /// </param>
    /// <returns>
    ///     A <see cref="MessageResource" /> object representing the provided message.
    /// </returns>
    public static MessageResource ToResourceFromEntity(Message message)
    {
        return new MessageResource(message.Id, message.Content, message.SenderType, message.SenderId, message.SentAt);
    }
}
