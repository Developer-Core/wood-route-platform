using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform Manufacturing domain errors and results into HTTP action results.
/// </summary>
/// <remarks>
///     Not-found errors map to 404, unauthorized updates to 403, invalid state transitions to
///     409 and any other domain error to 400, all rendered as RFC 7807 problem details.
/// </remarks>
public static class ManufacturingActionResultAssembler
{
    /// <summary>
    ///     Converts a result to the success action result or a problem details response.
    /// </summary>
    public static IActionResult ToActionResultFromResult<T>(ControllerBase controller,
        ProblemDetailsFactory problemDetailsFactory, Result<T> result, Func<T, IActionResult> successAction)
    {
        return result.IsSuccess
            ? successAction(result.Value)
            : problemDetailsFactory.CreateFromError(controller, ToStatusCode(result.Error), result.Error);
    }

    /// <summary>
    ///     Maps a domain error to its corresponding HTTP status code.
    /// </summary>
    public static int ToStatusCode(Error error)
    {
        return error.Code switch
        {
            var code when code == ManufacturingErrors.ManufactureOrderNotFound.Code => StatusCodes.Status404NotFound,
            var code when code == ManufacturingErrors.StageNotFound.Code => StatusCodes.Status404NotFound,
            var code when code == ManufacturingErrors.UnauthorizedStageUpdate.Code => StatusCodes.Status403Forbidden,
            var code when code == ManufacturingErrors.OrderNotAccepted.Code => StatusCodes.Status409Conflict,
            var code when code == ManufacturingErrors.StagesAlreadyDefined.Code => StatusCodes.Status409Conflict,
            var code when code == ManufacturingErrors.StagesAlreadyStarted.Code => StatusCodes.Status409Conflict,
            var code when code == ManufacturingErrors.InvalidStageTransition.Code => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };
    }
}
