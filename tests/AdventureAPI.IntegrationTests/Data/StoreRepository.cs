using AdventureAPI.Core.Aggregates.StoreAggregate;
using AdventureAPI.Core.Aggregates.StoreAggregate.Specifications;
using AdventureAPI.Infrastructure.Data;

namespace AdventureAPI.IntegrationTests.Data;

public class StoreRepository(BaseEfRepoTestFixture fixture)
    : IClassFixture<BaseEfRepoTestFixture>
{
    [Fact]
    public async Task AddStore_WithStoreName_CreatesNewStore()
    {
        const string testStoreName = "testStore";
        var testStoreStatus = StoreStatus.Pending;
        var repository = new EfRepository<Store>(fixture.DbContext);
        var store = new Store(testStoreName, "Test User");

        await repository.AddAsync(store);

        var spec = new StoreByIdSpec(store.Id);
        var newStore = await repository.FirstOrDefaultAsync(spec);

        Assert.Equal(testStoreName, newStore?.Name);
        Assert.Equal(testStoreStatus, newStore?.Status);
    }
}
