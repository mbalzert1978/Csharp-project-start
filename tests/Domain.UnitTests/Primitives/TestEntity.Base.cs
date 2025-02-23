using Domain.Primitives;

namespace Domain.UnitTests.Primitives;

public partial class EntityTests
{
    private sealed class FakeEntity(Guid id) : EntityBase<Guid>(id) { }

    private sealed class IntFakeEntity() : EntityBase<int>(42) { }

    private static (FakeEntity, FakeEntity, IntFakeEntity) FetchEntities(Guid? id = null)
    {
        FakeEntity a = new(id ?? Guid.CreateVersion7());
        FakeEntity b = new(id ?? Guid.CreateVersion7());
        IntFakeEntity c = new();
        return (a, b, c);
    }
}
