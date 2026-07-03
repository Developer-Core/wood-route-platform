namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource representing a user.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="Email">The email address of the user.</param>
/// <param name="Role">The role of the user.</param>
public record UserResource(int Id, string Email, string Role);
