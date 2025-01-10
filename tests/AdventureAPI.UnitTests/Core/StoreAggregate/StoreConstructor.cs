namespace AdventureAPI.UnitTests.Core.StoreAggregate;

public class StoreConstructor
{
    private readonly string _testName = "test name";
    private Store? _testStore;

    public Store CreateStore()
    {
        return new Store(_testName, "test user");
    }

    [Fact]
    public void InitializesName()
    {
        _testStore = CreateStore();
        Assert.Equal(_testName, _testStore.Name);
    }
}
