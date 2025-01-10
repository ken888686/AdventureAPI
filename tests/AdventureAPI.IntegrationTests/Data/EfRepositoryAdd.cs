using AdventureAPI.Core.Aggregates.StoreAggregate;
using AdventureAPI.Core.Enums;

namespace AdventureAPI.IntegrationTests.Data;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
    [Fact]
    public async Task AddStore_WithStoreName_CreatesNewStore()
    {
        const string testStoreName = "testStore";
        var testStoreStatus = StoreStatus.Pending;
        var repository = GetRepository();
        var store = new Store(testStoreName, "Test User");

        await repository.AddAsync(store);

        var newStore = (await repository.ListAsync())
            .FirstOrDefault();

        Assert.Equal(testStoreName, newStore?.Name);
        Assert.Equal(testStoreStatus, newStore?.Status);
    }
}
