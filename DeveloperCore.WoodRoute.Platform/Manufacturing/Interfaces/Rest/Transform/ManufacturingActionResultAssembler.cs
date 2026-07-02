using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
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
    public static IActionResult ToActionResultFromResult<T>(ControllerBase controller, Result<T> result,
        Func<T, IActionResult> successAction)
    {
        return result.IsSuccess ? successAction(result.Value) : ToProblemFromError(controller, result.Error);
    }

    /// <summary>
    ///     Converts a domain error to a problem details response.
    /// </summary>
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
            var code when code == ManufacturingErrors.ManufactureOrderNotFound.Code => StatusCodes.Status404NotFound,
            var code when code == ManufacturingErrors.StageNotFound.Code => StatusCodes.Status404NotFound,
            var code when code == ManufacturingErrors.UnauthorizedStageUpdate.Code => StatusCodes.Status403Forbidden,
            var code when code == ManufacturingErrors.OrderNotAccepted.Code => StatusCodes.Status409Conflict,
            var code when code == ManufacturingErrors.StagesAlreadyDefined.Code => StatusCodes.Status409Conflict,
            var code when code == ManufacturingErrors.InvalidStageTransition.Code => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };
    }
}
