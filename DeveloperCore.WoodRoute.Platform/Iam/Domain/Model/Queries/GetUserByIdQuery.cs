namespace DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Queries;

/// <summary>
///     Query to retrieve a user by its unique identifier.
/// </summary>
/// <param name="Id">The identifier of the user to search for.</param>
public record GetUserByIdQuery(int Id);
