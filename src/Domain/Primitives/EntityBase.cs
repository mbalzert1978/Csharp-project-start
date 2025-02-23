using System.Diagnostics.CodeAnalysis;

namespace Domain.Primitives;

public abstract class EntityBase<TId>(TId id) : IEquatable<EntityBase<TId>>
    where TId : notnull, IEquatable<TId>
{
    public TId Id { get; } = id;

    public static bool operator ==(EntityBase<TId>? left, EntityBase<TId>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EntityBase<TId>? left, EntityBase<TId>? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj) => Equals(obj as EntityBase<TId>);

    public bool Equals(EntityBase<TId>? other) => other is not null && Id.Equals(other.Id);

    public override int GetHashCode() => HashCode.Combine(42, Id);
}
