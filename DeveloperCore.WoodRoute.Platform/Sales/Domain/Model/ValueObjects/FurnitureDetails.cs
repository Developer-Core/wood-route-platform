namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;

/// <summary>
///     Value object describing the custom furniture piece requested in an order.
/// </summary>
/// <remarks>
///     All dimensions are expressed in centimeters and must be positive values.
/// </remarks>
public record FurnitureDetails
{
    /// <summary>
    ///     Creates the furniture details for an order.
    /// </summary>
    /// <param name="furnitureType">
    ///     The type of furniture requested.
    /// </param>
    /// <param name="width">
    ///     The width of the furniture piece, in centimeters.
    /// </param>
    /// <param name="height">
    ///     The height of the furniture piece, in centimeters.
    /// </param>
    /// <param name="depth">
    ///     The depth of the furniture piece, in centimeters.
    /// </param>
    /// <param name="material">
    ///     The material requested for the furniture piece.
    /// </param>
    /// <param name="designNotes">
    ///     Additional design notes for the furniture piece.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when the <paramref name="width" />, <paramref name="height" /> or <paramref name="depth" /> is not a
    ///     positive value.
    /// </exception>
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
