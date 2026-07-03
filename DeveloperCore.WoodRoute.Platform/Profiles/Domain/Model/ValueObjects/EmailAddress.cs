namespace DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.ValueObjects;

/// <summary>
///     Value object describing a validated email address.
/// </summary>
public record EmailAddress
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EmailAddress" /> value object.
    /// </summary>
    /// <param name="address">
    ///     The email address. Must be a non-empty value containing an '@' character.
    /// </param>
    /// <exception cref="ArgumentException">Thrown if the input <paramref name="address" /> is not a valid email address.</exception>
    public EmailAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || !address.Contains('@'))
            throw new ArgumentException("A valid email address is required.", nameof(address));
        Address = address;
    }

    public string Address { get; }
}
