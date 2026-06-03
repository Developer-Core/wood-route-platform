namespace DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.ValueObjects;

/// <summary>
///     Base class for all Value Objects in the WoodRoute Platform.
/// </summary>
/// <remarks>
///     A Value Object has no conceptual identity. Two value objects are equal if all
///     of their component properties are equal (structural equality).
///     <para>
///         Value Objects should be:
///         <list type="bullet">
///             <item>Immutable — never change state after creation.</item>
///             <item>Self-validating — throw on invalid input in the constructor or factory method.</item>
///             <item>Side-effect free — methods should not change internal state.</item>
///         </list>
///     </para>
///     Example:
///     <code>
///     public class Money : ValueObject
///     {
///         public decimal Amount { get; }
///         public string Currency { get; }
///
///         public Money(decimal amount, string currency)
///         {
///             if (amount &lt; 0) throw new ArgumentException("Amount cannot be negative.", nameof(amount));
///             Amount = amount;
///             Currency = currency;
///         }
///
///         protected override IEnumerable&lt;object?&gt; GetEqualityComponents()
///         {
///             yield return Amount;
///             yield return Currency;
///         }
///     }
///     </code>
/// </remarks>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <summary>
    ///     Returns the components that are used to determine equality.
    /// </summary>
    /// <remarks>
    ///     Override this method and yield all properties that define equality for this value object.
    /// </remarks>
    /// <returns>An enumerable of the equality components.</returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <inheritdoc />
    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(0, (current, component) =>
                HashCode.Combine(current, component?.GetHashCode() ?? 0));
    }

    /// <summary>Equality operator.</summary>
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    /// <summary>Inequality operator.</summary>
    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }
}
