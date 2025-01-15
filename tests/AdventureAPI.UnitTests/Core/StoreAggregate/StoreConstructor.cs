namespace AdventureAPI.UnitTests.Core.StoreAggregate;

public class StoreConstructor
{
    private const string TestName = "test name";
    private Store? _testStore;

    private static Store CreateStore()
    {
        return new Store(TestName, "test user");
    }

    [Fact]
    public void InitializesName()
    {
        _testStore = CreateStore();
        Assert.Equal(TestName, _testStore.Name);
    }
}
