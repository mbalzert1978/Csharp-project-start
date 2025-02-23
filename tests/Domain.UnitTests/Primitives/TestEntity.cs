using Domain.Primitives;

namespace Domain.UnitTests.Primitives;

public partial class EntityTests
{
    [Fact]
    public void EntityWhenComparedToAnotherEntityShouldReturnTrueWhenTheyHaveTheSameId()
    {
        (FakeEntity a, FakeEntity b, _) = FetchEntities(Guid.NewGuid());

        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact]
    public void EntityWhenComparedToAnotherEntityShouldReturnFalseWhenTheyHaveNotTheSameId()
    {
        (FakeEntity a, FakeEntity b, _) = FetchEntities();

        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact]
    public void EntitiesOfDifferentTypesWhenComparedUsingEqualsMethodReturnFalse()
    {
        (FakeEntity a, _, IntFakeEntity c) = FetchEntities();

        Assert.False(a.Equals(c));
    }
}
