using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform Profiles domain errors and results into HTTP action results.
/// </summary>
/// <remarks>
///     Not-found errors map to 404, email conflicts map to 409 and any other domain error
///     maps to 400, all rendered as RFC 7807 problem details.
/// </remarks>
public static class ProfilesActionResultAssembler
{
    /// <summary>
    ///     Converts a profile result to the success action result or a problem details response.
    /// </summary>
    /// <param name="controller">
    ///     The controller used to build the action result.
    /// </param>
    /// <param name="result">
    ///     The <see cref="Result{T}" /> wrapping the profile outcome to convert.
    /// </param>
    /// <param name="successAction">
    ///     The factory that builds the success action result from the resulting profile.
    /// </param>
    /// <returns>
    ///     The success <see cref="IActionResult" /> when the result is successful; otherwise a problem details response.
    /// </returns>
    public static IActionResult ToActionResultFromResult(ControllerBase controller, Result<Profile> result,
        Func<Profile, IActionResult> successAction)
    {
        return result.IsSuccess ? successAction(result.Value) : ToProblemFromError(controller, result.Error);
    }

    /// <summary>
    ///     Converts a domain error to a problem details response.
    /// </summary>
    /// <param name="controller">
    ///     The controller used to build the problem details response.
    /// </param>
    /// <param name="error">
    ///     The domain <see cref="Error" /> to convert.
    /// </param>
    /// <returns>
    ///     An <see cref="IActionResult" /> rendering the error as an RFC 7807 problem details response.
    /// </returns>
    public static IActionResult ToProblemFromError(ControllerBase controller, Error error)
    {
        return controller.Problem(
            statusCode: ToStatusCodeFromError(error),
            title: error.Code,
            detail: error.Message,
            instance: controller.HttpContext.Request.Path);
    }

    private static int ToStatusCodeFromError(Error error)
    {
        return error.Code switch
        {
            var code when code == ProfileErrors.ProfileNotFound.Code => StatusCodes.Status404NotFound,
            var code when code == ProfileErrors.EmailAlreadyRegistered.Code => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };
    }
}
