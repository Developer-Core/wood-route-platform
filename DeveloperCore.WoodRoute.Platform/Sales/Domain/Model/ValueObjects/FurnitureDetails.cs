namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;

/// <summary>
///     Value object describing the custom furniture piece requested in an order.
/// </summary>
/// <remarks>
///     All dimensions are expressed in centimeters and must be positive values.
/// </remarks>
public record FurnitureDetails
{
    public FurnitureDetails(string furnitureType, decimal width, decimal height, decimal depth, string material,
        string designNotes)
    {
        if (width <= 0)
            throw new ArgumentOutOfRangeException(nameof(width), "Furniture width must be a positive value.");
        if (height <= 0)
            throw new ArgumentOutOfRangeException(nameof(height), "Furniture height must be a positive value.");
        if (depth <= 0)
            throw new ArgumentOutOfRangeException(nameof(depth), "Furniture depth must be a positive value.");
        FurnitureType = furnitureType;
        Width = width;
        Height = height;
        Depth = depth;
        Material = material;
        DesignNotes = designNotes;
    }

    public string FurnitureType { get; }
    public decimal Width { get; }
    public decimal Height { get; }
    public decimal Depth { get; }
    public string Material { get; }
    public string DesignNotes { get; }
}
