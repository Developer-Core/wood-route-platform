using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Aggregates;

/// <summary>
///     Profile Aggregate Root
/// </summary>
/// <remarks>
///     Represents the personal information of a platform user. Updates to the profile
///     are applied through guarded behavior that returns a domain <see cref="Error" />
///     (<see cref="Error.None" /> on success).
/// </remarks>
public class Profile : IAuditableEntity
{
    private Profile()
    {
        Name = null!;
        Email = null!;
    }

    public Profile(CreateProfileCommand command)
    {
        Name = new PersonName(command.FirstName, command.LastName);
        Email = new EmailAddress(command.Email);
    }

    public int Id { get; }
    public PersonName Name { get; private set; }
    public EmailAddress Email { get; private set; }

    /// <summary>
    ///     Gets the full name of the profile owner.
    /// </summary>
    public string FullName => Name.FullName;

    /// <summary>
    ///     Gets the email address of the profile owner.
    /// </summary>
    public string EmailAddress => Email.Address;

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    ///     Updates the personal information of the profile.
    /// </summary>
    public Error Update(UpdateProfileCommand command)
    {
        Name = new PersonName(command.FirstName, command.LastName);
        Email = new EmailAddress(command.Email);
        return Error.None;
    }
}
