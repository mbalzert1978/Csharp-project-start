using System.Diagnostics.CodeAnalysis;

namespace Domain.Primitives;


public abstract class EntityBase<TId>(TId id) : IEquatable<EntityBase<TId>>
    where TId : notnull, IEquatable<TId>
{
    public TId Id { get; } = id;

    public static bool operator ==(EntityBase<TId>? left, EntityBase<TId>? right)
    {
        return left?.Equals(right) ?? false;
    }

    public static bool operator !=(EntityBase<TId>? left, EntityBase<TId>? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj) => obj?.Equals(this) ?? false;

    public bool Equals(EntityBase<TId>? other) => other?.Id.Equals(Id) ?? false;

    public override int GetHashCode() => Id.GetHashCode();

}
