using System.Net.Mime;
using DeveloperCore.WoodRoute.Platform.Inventory.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Inventory.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest.Resources;
using DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest.Transform;
using DeveloperCore.WoodRoute.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest;

/// <summary>
///     REST controller for inventory material management.
/// </summary>
[ApiController]
[Route("api/v1/inventory")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Inventory Endpoints.")]
public class InventoryMaterialsController(
    IInventoryMaterialCommandService inventoryMaterialCommandService,
    IInventoryMaterialQueryService inventoryMaterialQueryService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Inventory Material", "Register a new inventory material.",
        OperationId = "CreateInventoryMaterial")]
    [SwaggerResponse(201, "The inventory material was created.", typeof(InventoryMaterialResource))]
    [SwaggerResponse(400, "The inventory material was not created.")]
    public async Task<IActionResult> CreateInventoryMaterial(CreateInventoryMaterialResource resource,
        CancellationToken cancellationToken)
    {
        var createCommand = CreateInventoryMaterialCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await inventoryMaterialCommandService.Handle(createCommand, cancellationToken);

        return InventoryActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            createdMaterial => CreatedAtAction(nameof(GetInventoryMaterialById),
                new { materialId = createdMaterial.Id },
                InventoryMaterialResourceFromEntityAssembler.ToResourceFromEntity(createdMaterial)));
    }

    [HttpGet]
    [SwaggerOperation("Get All Inventory Materials", "Get all inventory materials.",
        OperationId = "GetAllInventoryMaterials")]
    [SwaggerResponse(200, "The inventory materials were found and returned.",
        typeof(IEnumerable<InventoryMaterialResource>))]
    public async Task<IActionResult> GetAllInventoryMaterials(CancellationToken cancellationToken)
    {
        var materials = await inventoryMaterialQueryService.Handle(new GetAllInventoryMaterialsQuery(),
            cancellationToken);
        var materialResources = materials.Select(InventoryMaterialResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(materialResources);
    }

    [HttpGet("{materialId:int}")]
    [SwaggerOperation("Get Inventory Material by Id", "Get an inventory material by its unique identifier.",
        OperationId = "GetInventoryMaterialById")]
    [SwaggerResponse(200, "The inventory material was found and returned.", typeof(InventoryMaterialResource))]
    [SwaggerResponse(404, "The inventory material was not found.")]
    public async Task<IActionResult> GetInventoryMaterialById(int materialId, CancellationToken cancellationToken)
    {
        var material = await inventoryMaterialQueryService.Handle(new GetInventoryMaterialByIdQuery(materialId),
            cancellationToken);

        if (material is null)
            return problemDetailsFactory.CreateFromError(this,
                InventoryActionResultAssembler.ToStatusCode(InventoryErrors.MaterialNotFound),
                InventoryErrors.MaterialNotFound);
        return Ok(InventoryMaterialResourceFromEntityAssembler.ToResourceFromEntity(material));
    }

    [HttpPatch("{materialId:int}")]
    [SwaggerOperation("Update Inventory Material", "Update the stock levels of an inventory material.",
        OperationId = "UpdateInventoryMaterial")]
    [SwaggerResponse(200, "The inventory material was updated.", typeof(InventoryMaterialResource))]
    [SwaggerResponse(400, "The stock values are invalid.")]
    [SwaggerResponse(404, "The inventory material was not found.")]
    public async Task<IActionResult> UpdateInventoryMaterial(int materialId,
        UpdateInventoryMaterialResource resource, CancellationToken cancellationToken)
    {
        var updateCommand =
            UpdateInventoryMaterialCommandFromResourceAssembler.ToCommandFromResource(materialId, resource);
        var result = await inventoryMaterialCommandService.Handle(updateCommand, cancellationToken);

        return InventoryActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            updatedMaterial => Ok(InventoryMaterialResourceFromEntityAssembler.ToResourceFromEntity(updatedMaterial)));
    }
}
