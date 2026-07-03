using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Application.CommandServices;

/// <summary>
///     Contract for the message command service.
/// </summary>
public interface IMessageCommandService
{
    /// <summary>
    ///     Handles the send message command.
    ///     Creates a new conversation for the order if one does not exist yet.
    /// </summary>
    Task<Result<Message>> Handle(SendMessageCommand command, CancellationToken cancellationToken = default);
}
