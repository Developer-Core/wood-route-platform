using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform Sales domain errors and results into HTTP action results.
/// </summary>
/// <remarks>
///     Not-found errors map to 404, invalid state transitions map to 409 and any
///     other domain error maps to 400, all rendered as RFC 7807 problem details.
/// </remarks>
public static class SalesActionResultAssembler
{
    /// <summary>
    ///     Converts an order result to the success action result or a problem details response.
    /// </summary>
    public static IActionResult ToActionResultFromResult(ControllerBase controller, Result<Order> result,
        Func<Order, IActionResult> successAction)
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
            var code when code == SalesErrors.OrderNotFound.Code => StatusCodes.Status404NotFound,
            var code when code == SalesErrors.QuoteNotFound.Code => StatusCodes.Status404NotFound,
            var code when code == SalesErrors.PaymentNotFound.Code => StatusCodes.Status404NotFound,
            var code when code == SalesErrors.OrderNotPending.Code => StatusCodes.Status409Conflict,
            var code when code == SalesErrors.OrderNotPayable.Code => StatusCodes.Status409Conflict,
            var code when code == SalesErrors.QuoteAlreadyExists.Code => StatusCodes.Status409Conflict,
            var code when code == SalesErrors.QuoteAlreadyAccepted.Code => StatusCodes.Status409Conflict,
            var code when code == SalesErrors.QuoteAlreadyDecided.Code => StatusCodes.Status409Conflict,
            var code when code == SalesErrors.PaymentAlreadyValidated.Code => StatusCodes.Status409Conflict,
            "Sales.InvalidOrderStatusTransition" => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };
    }
}
