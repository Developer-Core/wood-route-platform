using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform Inventory domain errors and results into HTTP action results.
/// </summary>
/// <remarks>
///     Not-found errors map to 404, invalid stock values map to 400 and any other domain
///     error maps to 400, all rendered as RFC 7807 problem details.
/// </remarks>
public static class InventoryActionResultAssembler
{
    /// <summary>
    ///     Converts an inventory material result to the success action result or a problem details response.
    /// </summary>
    /// <param name="controller">
    ///     The <see cref="ControllerBase" /> used to build the response.
    /// </param>
    /// <param name="result">
    ///     The <see cref="Result{InventoryMaterial}" /> to convert.
    /// </param>
    /// <param name="successAction">
    ///     The action that produces the success <see cref="IActionResult" /> from the material.
    /// </param>
    /// <returns>
    ///     The success <see cref="IActionResult" /> when the result succeeds; otherwise a problem details response.
    /// </returns>
    public static IActionResult ToActionResultFromResult(ControllerBase controller, Result<InventoryMaterial> result,
        Func<InventoryMaterial, IActionResult> successAction)
    {
        return result.IsSuccess ? successAction(result.Value) : ToProblemFromError(controller, result.Error);
    }

    /// <summary>
    ///     Converts a domain error to a problem details response.
    /// </summary>
    /// <param name="controller">
    ///     The <see cref="ControllerBase" /> used to build the response.
    /// </param>
    /// <param name="error">
    ///     The domain <see cref="Error" /> to convert.
    /// </param>
    /// <returns>
    ///     A problem details <see cref="IActionResult" /> representing the error.
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
            var code when code == InventoryErrors.MaterialNotFound.Code => StatusCodes.Status404NotFound,
            var code when code == InventoryErrors.InvalidQuantity.Code => StatusCodes.Status400BadRequest,
            var code when code == InventoryErrors.InvalidMinimumStock.Code => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status400BadRequest
        };
    }
}
