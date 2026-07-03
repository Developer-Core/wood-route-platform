using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Interfaces.Rest.ProblemDetails;
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
    /// <param name="controller">
    ///     The <see cref="ControllerBase" /> producing the response.
    /// </param>
    /// <param name="result">
    ///     The <see cref="Result{Order}" /> to convert.
    /// </param>
    /// <param name="successAction">
    ///     The action producing the success result from the order.
    /// </param>
    /// <returns>
    ///     The success <see cref="IActionResult" /> when the result is successful, otherwise a problem details response.
    /// </returns>
    public static IActionResult ToActionResultFromResult(ControllerBase controller,
        ProblemDetailsFactory problemDetailsFactory, Result<Order> result, Func<Order, IActionResult> successAction)
    {
        return result.IsSuccess
            ? successAction(result.Value)
            : problemDetailsFactory.CreateFromError(controller, ToStatusCode(result.Error), result.Error);
    }

    /// <summary>
    ///     Maps a domain error to its corresponding HTTP status code.
    /// </summary>
    /// <param name="error">
    ///     The <see cref="Error" /> to map.
    /// </param>
    /// <returns>
    ///     The HTTP status code representing the error.
    /// </returns>
    public static int ToStatusCode(Error error)
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
