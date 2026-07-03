using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to map Engagement domain errors to HTTP status codes.
/// </summary>
/// <remarks>
///     Not-found errors map to 404 and any other domain error maps to 400.
///     The localized RFC 7807 problem details response is built by the shared
///     <see cref="Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory" />.
/// </remarks>
public static class EngagementActionResultAssembler
{
    /// <summary>
    ///     Maps a domain error to its corresponding HTTP status code.
    /// </summary>
    /// <param name="error">
    ///     The domain <see cref="Error" /> to map.
    /// </param>
    /// <returns>
    ///     The HTTP status code representing the error.
    /// </returns>
    public static int ToStatusCode(Error error)
    {
        return error.Code switch
        {
            var code when code == EngagementErrors.ConversationNotFound.Code => StatusCodes.Status404NotFound,
            var code when code == EngagementErrors.TrackingIdNotFound.Code => StatusCodes.Status404NotFound,
            var code when code == EngagementErrors.MessageContentEmpty.Code => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status400BadRequest
        };
    }
}
