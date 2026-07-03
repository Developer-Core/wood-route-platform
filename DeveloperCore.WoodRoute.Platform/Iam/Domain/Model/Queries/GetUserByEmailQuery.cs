namespace DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Queries;

/// <summary>
///     Query to retrieve a user by its email address.
/// </summary>
/// <param name="Email">The email address of the user to search for.</param>
public record GetUserByEmailQuery(string Email);
