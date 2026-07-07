namespace DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.ValueObjects;

/// <summary>
///     Value object describing a person's first and last name.
/// </summary>
public record PersonName
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PersonName" /> value object.
    /// </summary>
    /// <param name="firstName">
    ///     The first name. Must not be null or empty.
    /// </param>
    /// <param name="lastName">
    ///     The last name. Must not be null or empty.
    /// </param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="firstName" /> or <paramref name="lastName" /> is null or empty.</exception>
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
