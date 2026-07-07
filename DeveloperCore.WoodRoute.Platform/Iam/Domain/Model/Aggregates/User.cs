using System.Text.Json.Serialization;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;

/// <summary>
///     User Aggregate Root
/// </summary>
/// <remarks>
///     Represents an authenticated identity on the WoodRoute Platform. A user owns a unique
///     email, a BCrypt password hash and a role (<see cref="EUserRole" />) chosen at registration.
///     Credentials are managed through guarded behavior and the password hash is never serialized.
/// </remarks>
public class User : IAuditableEntity
{
    private User()
    {
        Email = string.Empty;
        PasswordHash = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="User" /> aggregate from a sign-up command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="SignUpCommand" /> carrying the email and chosen role.
    /// </param>
    /// <param name="passwordHash">
    ///     The already hashed password produced by the hashing service.
    /// </param>
    public User(SignUpCommand command, string passwordHash)
    {
        Email = command.Email;
        PasswordHash = passwordHash;
        Role = command.Role;
        FirstName = command.FirstName;
        LastName = command.LastName;
    }

    public int Id { get; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    /// <summary>
    ///     Gets the full name of the user, composed from <see cref="FirstName" /> and <see cref="LastName" />.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    [JsonIgnore] public string PasswordHash { get; private set; }

    public EUserRole Role { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    ///     Updates the email address of the user.
    /// </summary>
    /// <param name="email">
    ///     The new email address.
    /// </param>
    /// <returns>
    ///     The updated <see cref="User" /> instance.
    /// </returns>
    public User UpdateEmail(string email)
    {
        Email = email;
        return this;
    }

    /// <summary>
    ///     Updates the password hash of the user.
    /// </summary>
    /// <param name="passwordHash">
    ///     The new password hash.
    /// </param>
    /// <returns>
    ///     The updated <see cref="User" /> instance.
    /// </returns>
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
}
