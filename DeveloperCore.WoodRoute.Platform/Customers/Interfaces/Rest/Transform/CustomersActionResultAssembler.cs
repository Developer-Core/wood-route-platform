using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform Customers domain errors and results into HTTP action results.
/// </summary>
/// <remarks>
///     Not-found errors map to 404, email conflicts map to 409 and any other domain error
///     maps to 400, all rendered as RFC 7807 problem details.
/// </remarks>
public static class CustomersActionResultAssembler
{
    /// <summary>
    ///     Converts a customer result to the success action result or a problem details response.
    /// </summary>
    /// <param name="controller">
    ///     The controller used to build the action result.
    /// </param>
    /// <param name="result">
    ///     The <see cref="Result{T}" /> wrapping the customer outcome to convert.
    /// </param>
    /// <param name="successAction">
    ///     The factory that builds the success action result from the resulting customer.
    /// </param>
    /// <returns>
    ///     The success <see cref="IActionResult" /> when the result is successful; otherwise a problem details response.
    /// </returns>
    public static IActionResult ToActionResultFromResult(ControllerBase controller,
        ProblemDetailsFactory problemDetailsFactory, Result<Customer> result,
        Func<Customer, IActionResult> successAction)
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
            var code when code == CustomerErrors.CustomerNotFound.Code => StatusCodes.Status404NotFound,
            var code when code == CustomerErrors.EmailAlreadyRegistered.Code => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };
    }
}
