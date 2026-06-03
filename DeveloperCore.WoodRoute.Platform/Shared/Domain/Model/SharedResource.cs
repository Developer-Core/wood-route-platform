namespace DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;

/// <summary>
///     Marker class used by <see cref="Microsoft.Extensions.Localization.IStringLocalizer{T}" />
///     to resolve the shared resource file located at <c>Resources/SharedResource.resx</c>.
/// </summary>
/// <remarks>
///     This class intentionally has no members. Its only purpose is to serve as a type
///     parameter for <c>IStringLocalizer&lt;SharedResource&gt;</c>, allowing the localizer
///     to load messages from the shared resource files across all bounded contexts.
/// </remarks>
public class SharedResource;
