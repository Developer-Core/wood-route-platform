namespace DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.ValueObjects;

/// <summary>
///     Value object describing a person's first and last name.
/// </summary>
public record PersonName
{
    public PersonName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("The first name is required.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("The last name is required.", nameof(lastName));
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; }
    public string LastName { get; }

    /// <summary>
    ///     Gets the full name composed from the first and last name.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";
}
