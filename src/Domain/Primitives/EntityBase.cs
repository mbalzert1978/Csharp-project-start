using System.Diagnostics.CodeAnalysis;

namespace Domain.Primitives;

public abstract class EntityBase<TId>(TId id)
    : IEqualityComparer<EntityBase<TId>>,
        IEquatable<EntityBase<TId>>
    where TId : notnull
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

    public bool Equals(EntityBase<TId>? other) => other?.Equals(this) ?? false;

    public bool Equals(EntityBase<TId>? x, EntityBase<TId>? y) => x?.Equals(y) ?? false;

    public sealed override bool Equals(object? obj) =>
        obj is EntityBase<TId> entity && entity.Id.Equals(Id);

    public sealed override int GetHashCode() => Id.GetHashCode();

    public int GetHashCode([DisallowNull] EntityBase<TId> obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        return obj.GetHashCode();
    }
}
