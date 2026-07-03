namespace DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.ValueObjects;

/// <summary>
///     Represents the role a user plays on the WoodRoute Platform.
/// </summary>
/// <remarks>
///     The role is chosen at registration (HU16 / TS09) and determines whether the user
///     acts as a carpenter offering custom furniture services or as a client requesting them.
/// </remarks>
public enum EUserRole
{
    Carpenter,
    Client
}
