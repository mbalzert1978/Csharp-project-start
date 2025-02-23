using Domain.Primitives;

namespace Domain.UnitTests.Primitives;

public class EntityTests
{
    [Fact]
    public void EntityWhenComparedToAnotherEntityShouldReturnFalseWhenTheyAreNotTheSameType()
    {
        (FakeEntityA a, FakeEntityB b) = Setup();

        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact]
    public void EntitiesOfDifferentTypesWhenComparedUsingEqualsMethodReturnFalse()
    {
        (FakeEntityA a, FakeEntityB b) = Setup();

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EntitiesOfDifferentTypesWhenComparedUsingObjectReferenceEqualsReturnFalse()
    {
        (FakeEntityA a, FakeEntityB b) = Setup();

        Assert.False(ReferenceEquals(a, b));
    }

    private sealed class FakeEntityA(Guid id) : EntityBase<Guid>(id) { }

    private sealed class FakeEntityB(Guid id) : EntityBase<Guid>(id) { }

    private static (FakeEntityA, FakeEntityB) Setup()
    {
        FakeEntityA a = new(Guid.NewGuid());
        FakeEntityB b = new(Guid.NewGuid());
        return (a, b);
    }
}
