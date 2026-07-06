using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform Iam domain errors and results into HTTP action results.
/// </summary>
/// <remarks>
///     Invalid credentials map to 401, email conflicts map to 409, not-found errors map to 404
///     and any other domain error maps to 400, all rendered as RFC 7807 problem details.
/// </remarks>
public static class IamActionResultAssembler
{
    /// <summary>
    ///     Converts an authenticated user result to the success action result or a problem details response.
    /// </summary>
    /// <param name="controller">
    ///     The <see cref="ControllerBase" /> producing the response.
    /// </param>
    /// <param name="problemDetailsFactory">
    ///     The factory used to build localized problem details responses.
    /// </param>
    /// <param name="result">
    ///     The <see cref="Result{T}" /> wrapping the authenticated user and its token.
    /// </param>
    /// <param name="successAction">
    ///     The factory that builds the success action result from the authenticated user and token.
    /// </param>
    /// <returns>
    ///     The success <see cref="IActionResult" /> when the result is successful; otherwise a problem details response.
    /// </returns>
    public static IActionResult ToActionResultFromResult(ControllerBase controller,
        ProblemDetailsFactory problemDetailsFactory, Result<(User user, string token)> result,
        Func<(User user, string token), IActionResult> successAction)
    {
        return result.IsSuccess
            ? successAction(result.Value)
            : problemDetailsFactory.CreateFromError(controller, ToStatusCode(result.Error), result.Error);
    }

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
            var code when code == IamErrors.InvalidCredentials.Code => StatusCodes.Status401Unauthorized,
            var code when code == IamErrors.EmailAlreadyRegistered.Code => StatusCodes.Status409Conflict,
            var code when code == IamErrors.UserNotFound.Code => StatusCodes.Status404NotFound,
            var code when code == IamErrors.InvalidInvitationCode.Code => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status400BadRequest
        };
    }
}
