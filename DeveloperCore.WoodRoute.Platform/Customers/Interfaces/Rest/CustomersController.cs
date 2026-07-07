using System.Net.Mime;
using DeveloperCore.WoodRoute.Platform.Customers.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Customers.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Rest.Resources;
using DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Rest.Transform;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using DeveloperCore.WoodRoute.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Rest;

/// <summary>
///     REST controller for workshop customer (CRM) management.
/// </summary>
/// <remarks>
///     Customer endpoints are restricted to carpenters, who own the client relationship of the
///     workshop, through a class-level <see cref="AuthorizeAttribute" />.
/// </remarks>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize(EUserRole.Carpenter)]
[SwaggerTag("Available Customer Endpoints.")]
public class CustomersController(
    ICustomerCommandService customerCommandService,
    ICustomerQueryService customerQueryService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Customer", "Create a new customer.", OperationId = "CreateCustomer")]
    [SwaggerResponse(201, "The customer was created.", typeof(CustomerResource))]
    [SwaggerResponse(400, "The customer data is invalid.")]
    [SwaggerResponse(409, "The email address is already registered.")]
    public async Task<IActionResult> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken)
    {
        var createCustomerCommand = CreateCustomerCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await customerCommandService.Handle(createCustomerCommand, cancellationToken);

        return CustomersActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            createdCustomer => CreatedAtAction(nameof(GetCustomerById), new { customerId = createdCustomer.Id },
                CustomerResourceFromEntityAssembler.ToResourceFromEntity(createdCustomer)));
    }

    [HttpGet]
    [SwaggerOperation("Get All Customers", "Get all customers.", OperationId = "GetAllCustomers")]
    [SwaggerResponse(200, "The customers were found and returned.", typeof(IEnumerable<CustomerResource>))]
    public async Task<IActionResult> GetAllCustomers(CancellationToken cancellationToken)
    {
        var customers = await customerQueryService.Handle(new GetAllCustomersQuery(), cancellationToken);
        var customerResources = customers.Select(CustomerResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(customerResources);
    }

    [HttpGet("{customerId:int}")]
    [SwaggerOperation("Get Customer by Id", "Get a customer by its unique identifier.",
        OperationId = "GetCustomerById")]
    [SwaggerResponse(200, "The customer was found and returned.", typeof(CustomerResource))]
    [SwaggerResponse(404, "The customer was not found.")]
    public async Task<IActionResult> GetCustomerById(int customerId, CancellationToken cancellationToken)
    {
        var getCustomerByIdQuery = new GetCustomerByIdQuery(customerId);
        var customer = await customerQueryService.Handle(getCustomerByIdQuery, cancellationToken);

        if (customer is null)
            return problemDetailsFactory.CreateFromError(this,
                CustomersActionResultAssembler.ToStatusCode(CustomerErrors.CustomerNotFound),
                CustomerErrors.CustomerNotFound);
        return Ok(CustomerResourceFromEntityAssembler.ToResourceFromEntity(customer));
    }

    [HttpPut("{customerId:int}")]
    [SwaggerOperation("Update Customer", "Update the contact information of a customer.",
        OperationId = "UpdateCustomer")]
    [SwaggerResponse(200, "The customer was updated.", typeof(CustomerResource))]
    [SwaggerResponse(400, "The customer data is invalid.")]
    [SwaggerResponse(404, "The customer was not found.")]
    public async Task<IActionResult> UpdateCustomer(int customerId, UpdateCustomerResource resource,
        CancellationToken cancellationToken)
    {
        var updateCustomerCommand =
            UpdateCustomerCommandFromResourceAssembler.ToCommandFromResource(customerId, resource);
        var result = await customerCommandService.Handle(updateCustomerCommand, cancellationToken);

        return CustomersActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            updatedCustomer => Ok(CustomerResourceFromEntityAssembler.ToResourceFromEntity(updatedCustomer)));
    }
}
