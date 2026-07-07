using System.Net.Mime;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using DeveloperCore.WoodRoute.Platform.Sales.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Sales.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Transform;
using DeveloperCore.WoodRoute.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest;

/// <summary>
///     REST controller for custom furniture order management.
/// </summary>
/// <remarks>
///     Order endpoints are available to any authenticated user, since both carpenters and clients
///     interact with orders. A few quote and payment actions are further restricted to a single
///     role through a method-level <see cref="AuthorizeAttribute" /> that takes precedence over the
///     generic class-level one.
/// </remarks>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize]
[SwaggerTag("Available Order Endpoints.")]
public class OrdersController(
    IOrderCommandService orderCommandService,
    IOrderQueryService orderQueryService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Order", "Create a new custom furniture order.", OperationId = "CreateOrder")]
    [SwaggerResponse(201, "The order was created.", typeof(OrderResource))]
    [SwaggerResponse(400, "The order was not created.")]
    public async Task<IActionResult> CreateOrder(CreateOrderResource resource, CancellationToken cancellationToken)
    {
        var createOrderCommand = CreateOrderCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await orderCommandService.Handle(createOrderCommand, cancellationToken);

        return SalesActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            createdOrder => CreatedAtAction(nameof(GetOrderById), new { orderId = createdOrder.Id },
                OrderResourceFromEntityAssembler.ToResourceFromEntity(createdOrder)));
    }

    [HttpGet]
    [SwaggerOperation("Get All Orders", "Get the orders that belong to the authenticated user.",
        OperationId = "GetAllOrders")]
    [SwaggerResponse(200, "The orders were found and returned.", typeof(IEnumerable<OrderResource>))]
    [SwaggerResponse(401, "The request is not authenticated.")]
    public async Task<IActionResult> GetAllOrders([FromQuery] int? customerId, [FromQuery] int? carpenterId,
        CancellationToken cancellationToken)
    {
        // Security (IDOR): the scope is derived exclusively from the authenticated identity in the
        // JWT-backed token. The client-supplied customerId/carpenterId query parameters are kept for
        // backward compatibility but deliberately ignored — trusting them would let any client read
        // every other user's orders.
        var user = HttpContext.GetAuthenticatedUser();
        if (user is null)
            return Unauthorized();

        var orders = user.Role == EUserRole.Client
            ? await orderQueryService.Handle(new GetOrdersByCustomerIdQuery(user.Id), cancellationToken)
            : await orderQueryService.Handle(new GetOrdersByCarpenterIdQuery(user.Id), cancellationToken);

        var orderResources = orders.Select(OrderResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(orderResources);
    }

    [HttpGet("pool")]
    [Authorize(EUserRole.Carpenter)]
    [SwaggerOperation("Get Order Pool", "Get the unassigned, pending orders any carpenter can claim.",
        OperationId = "GetOrderPool")]
    [SwaggerResponse(200, "The pool orders were found and returned.", typeof(IEnumerable<OrderResource>))]
    [SwaggerResponse(401, "The request is not authenticated.")]
    public async Task<IActionResult> GetOrderPool(CancellationToken cancellationToken)
    {
        var orders = await orderQueryService.Handle(new GetUnassignedOrdersQuery(), cancellationToken);
        var orderResources = orders.Select(OrderResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(orderResources);
    }

    [HttpGet("{orderId:int}")]
    [SwaggerOperation("Get Order by Id", "Get an order by its unique identifier.", OperationId = "GetOrderById")]
    [SwaggerResponse(200, "The order was found and returned.", typeof(OrderResource))]
    [SwaggerResponse(404, "The order was not found.")]
    public async Task<IActionResult> GetOrderById(int orderId, CancellationToken cancellationToken)
    {
        var user = HttpContext.GetAuthenticatedUser();
        if (user is null)
            return Unauthorized();

        var getOrderByIdQuery = new GetOrderByIdQuery(orderId);
        var order = await orderQueryService.Handle(getOrderByIdQuery, cancellationToken);

        // Security (IDOR): treat an order the user does not own exactly like a missing one, so the
        // response never leaks the existence of other users' orders.
        if (order is null || !OwnsOrder(user, order))
            return problemDetailsFactory.CreateFromError(this,
                SalesActionResultAssembler.ToStatusCode(SalesErrors.OrderNotFound), SalesErrors.OrderNotFound);
        return Ok(OrderResourceFromEntityAssembler.ToResourceFromEntity(order));
    }

    /// <summary>
    ///     Returns whether the authenticated user owns the order, based on their role: a client owns
    ///     the orders they placed, a carpenter owns the orders assigned to them.
    /// </summary>
    private static bool OwnsOrder(User user, Order order)
    {
        return user.Role == EUserRole.Client
            ? order.CustomerId == user.Id
            : order.CarpenterId == user.Id;
    }

    /// <summary>
    ///     Ensures the authenticated user owns the order before an action operates on it. Returns a
    ///     problem-details result to short-circuit the action when access is denied (rendered as a
    ///     404 not-found so the existence of other users' orders is never leaked), or <c>null</c>
    ///     when access is granted.
    /// </summary>
    private async Task<IActionResult?> EnsureOrderOwnershipAsync(int orderId, CancellationToken cancellationToken)
    {
        var user = HttpContext.GetAuthenticatedUser();
        if (user is null)
            return Unauthorized();

        var order = await orderQueryService.Handle(new GetOrderByIdQuery(orderId), cancellationToken);
        if (order is null || !OwnsOrder(user, order))
            return problemDetailsFactory.CreateFromError(this,
                SalesActionResultAssembler.ToStatusCode(SalesErrors.OrderNotFound), SalesErrors.OrderNotFound);
        return null;
    }

    [HttpPatch("{orderId:int}")]
    [SwaggerOperation("Modify Order", "Modify the furniture details of a pending order.",
        OperationId = "ModifyOrder")]
    [SwaggerResponse(200, "The order was modified.", typeof(OrderResource))]
    [SwaggerResponse(404, "The order was not found.")]
    [SwaggerResponse(409, "The order is not pending.")]
    public async Task<IActionResult> ModifyOrder(int orderId, UpdateOrderResource resource,
        CancellationToken cancellationToken)
    {
        var accessDenied = await EnsureOrderOwnershipAsync(orderId, cancellationToken);
        if (accessDenied is not null)
            return accessDenied;

        var modifyOrderCommand = ModifyOrderCommandFromResourceAssembler.ToCommandFromResource(orderId, resource);
        var result = await orderCommandService.Handle(modifyOrderCommand, cancellationToken);

        return SalesActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            modifiedOrder => Ok(OrderResourceFromEntityAssembler.ToResourceFromEntity(modifiedOrder)));
    }

    [HttpPatch("{orderId:int}/accept")]
    [Authorize(EUserRole.Carpenter)]
    [SwaggerOperation("Accept Order", "Accept a pending order, claiming it from the pool when unassigned.",
        OperationId = "AcceptOrder")]
    [SwaggerResponse(200, "The order was accepted.", typeof(OrderResource))]
    [SwaggerResponse(404, "The order was not found.")]
    [SwaggerResponse(409, "The order is not pending or is already assigned.")]
    public async Task<IActionResult> AcceptOrder(int orderId, CancellationToken cancellationToken)
    {
        // A carpenter accepts an order to claim it from the pool: the acting identity is derived from
        // the JWT-backed token (never from client input) and, when the order is still unassigned, the
        // command assigns it to this carpenter before accepting it.
        var user = HttpContext.GetAuthenticatedUser();
        if (user is null)
            return Unauthorized();

        var result = await orderCommandService.Handle(new AcceptOrderCommand(orderId, user.Id), cancellationToken);

        return SalesActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            acceptedOrder => Ok(OrderResourceFromEntityAssembler.ToResourceFromEntity(acceptedOrder)));
    }

    [HttpPatch("{orderId:int}/reject")]
    [SwaggerOperation("Reject Order", "Reject a pending order.", OperationId = "RejectOrder")]
    [SwaggerResponse(200, "The order was rejected.", typeof(OrderResource))]
    [SwaggerResponse(404, "The order was not found.")]
    [SwaggerResponse(409, "The order is not pending.")]
    public async Task<IActionResult> RejectOrder(int orderId, CancellationToken cancellationToken)
    {
        var accessDenied = await EnsureOrderOwnershipAsync(orderId, cancellationToken);
        if (accessDenied is not null)
            return accessDenied;

        var result = await orderCommandService.Handle(new RejectOrderCommand(orderId), cancellationToken);

        return SalesActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            rejectedOrder => Ok(OrderResourceFromEntityAssembler.ToResourceFromEntity(rejectedOrder)));
    }

    [HttpPatch("{orderId:int}/cancel")]
    [SwaggerOperation("Cancel Order", "Cancel a pending order.", OperationId = "CancelOrder")]
    [SwaggerResponse(200, "The order was cancelled.", typeof(OrderResource))]
    [SwaggerResponse(404, "The order was not found.")]
    [SwaggerResponse(409, "The order is not pending.")]
    public async Task<IActionResult> CancelOrder(int orderId, CancellationToken cancellationToken)
    {
        var accessDenied = await EnsureOrderOwnershipAsync(orderId, cancellationToken);
        if (accessDenied is not null)
            return accessDenied;

        var result = await orderCommandService.Handle(new CancelOrderCommand(orderId), cancellationToken);

        return SalesActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            cancelledOrder => Ok(OrderResourceFromEntityAssembler.ToResourceFromEntity(cancelledOrder)));
    }

    [HttpPost("{orderId:int}/quote")]
    [Authorize(EUserRole.Carpenter)]
    [SwaggerOperation("Generate Quote", "Generate the quote for a pending order.", OperationId = "GenerateQuote")]
    [SwaggerResponse(201, "The quote was generated.", typeof(OrderResource))]
    [SwaggerResponse(400, "The quote data is invalid.")]
    [SwaggerResponse(404, "The order was not found.")]
    [SwaggerResponse(409, "The order is not pending or already has a quote.")]
    public async Task<IActionResult> GenerateQuote(int orderId, GenerateQuoteResource resource,
        CancellationToken cancellationToken)
    {
        var accessDenied = await EnsureOrderOwnershipAsync(orderId, cancellationToken);
        if (accessDenied is not null)
            return accessDenied;

        var generateQuoteCommand = GenerateQuoteCommandFromResourceAssembler.ToCommandFromResource(orderId, resource);
        var result = await orderCommandService.Handle(generateQuoteCommand, cancellationToken);

        return SalesActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            quotedOrder => CreatedAtAction(nameof(GetOrderById), new { orderId = quotedOrder.Id },
                OrderResourceFromEntityAssembler.ToResourceFromEntity(quotedOrder)));
    }

    [HttpPatch("{orderId:int}/quote/accept")]
    [Authorize(EUserRole.Client)]
    [SwaggerOperation("Accept Quote", "Accept the quote proposed for an order.", OperationId = "AcceptQuote")]
    [SwaggerResponse(200, "The quote was accepted.", typeof(OrderResource))]
    [SwaggerResponse(404, "The order was not found.")]
    [SwaggerResponse(409, "The quote does not exist or was already decided.")]
    public async Task<IActionResult> AcceptQuote(int orderId, CancellationToken cancellationToken)
    {
        var accessDenied = await EnsureOrderOwnershipAsync(orderId, cancellationToken);
        if (accessDenied is not null)
            return accessDenied;

        var result = await orderCommandService.Handle(new AcceptQuoteCommand(orderId), cancellationToken);

        return SalesActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            quotedOrder => Ok(OrderResourceFromEntityAssembler.ToResourceFromEntity(quotedOrder)));
    }

    [HttpPost("{orderId:int}/payments")]
    [Authorize(EUserRole.Client)]
    [SwaggerOperation("Register Payment", "Register a payment for an order, pending receipt validation.",
        OperationId = "RegisterPayment")]
    [SwaggerResponse(201, "The payment was registered.", typeof(OrderResource))]
    [SwaggerResponse(400, "The payment data is invalid.")]
    [SwaggerResponse(404, "The order was not found.")]
    [SwaggerResponse(409, "The order is not payable.")]
    public async Task<IActionResult> RegisterPayment(int orderId, RegisterPaymentResource resource,
        CancellationToken cancellationToken)
    {
        var accessDenied = await EnsureOrderOwnershipAsync(orderId, cancellationToken);
        if (accessDenied is not null)
            return accessDenied;

        var registerPaymentCommand =
            RegisterPaymentCommandFromResourceAssembler.ToCommandFromResource(orderId, resource);
        var result = await orderCommandService.Handle(registerPaymentCommand, cancellationToken);

        return SalesActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            paidOrder => CreatedAtAction(nameof(GetOrderById), new { orderId = paidOrder.Id },
                OrderResourceFromEntityAssembler.ToResourceFromEntity(paidOrder)));
    }

    [HttpPatch("{orderId:int}/payments/{paymentId:int}/validate")]
    [Authorize(EUserRole.Carpenter)]
    [SwaggerOperation("Validate Payment", "Validate a payment receipt, confirming or rejecting it.",
        OperationId = "ValidatePayment")]
    [SwaggerResponse(200, "The payment was validated.", typeof(OrderResource))]
    [SwaggerResponse(404, "The order or payment was not found.")]
    [SwaggerResponse(409, "The payment was already validated.")]
    public async Task<IActionResult> ValidatePayment(int orderId, int paymentId, ValidatePaymentResource resource,
        CancellationToken cancellationToken)
    {
        var accessDenied = await EnsureOrderOwnershipAsync(orderId, cancellationToken);
        if (accessDenied is not null)
            return accessDenied;

        var validatePaymentCommand =
            ValidatePaymentCommandFromResourceAssembler.ToCommandFromResource(orderId, paymentId, resource);
        var result = await orderCommandService.Handle(validatePaymentCommand, cancellationToken);

        return SalesActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            validatedOrder => Ok(OrderResourceFromEntityAssembler.ToResourceFromEntity(validatedOrder)));
    }
}
