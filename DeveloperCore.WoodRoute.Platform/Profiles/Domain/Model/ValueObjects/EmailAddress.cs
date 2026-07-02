namespace DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.ValueObjects;

/// <summary>
///     Value object describing a validated email address.
/// </summary>
public record EmailAddress
{
    public EmailAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || !address.Contains('@'))
            throw new ArgumentException("A valid email address is required.", nameof(address));
        Address = address;
    }

    public string Address { get; }
}
