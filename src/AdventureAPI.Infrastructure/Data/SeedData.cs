using AdventureAPI.Core.Aggregates.StoreAggregate;

namespace AdventureAPI.Infrastructure.Data;

public static class SeedData
{
    public static readonly Store Store1 = new("Store 1", "Aaron");
    public static readonly Store Store2 = new("Store 2", "Annabelle");

    public static async Task InitializeAsync(AppDbContext dbContext)
    {
        await PopulateTestDataAsync(dbContext);
    }

    public static async Task PopulateTestDataAsync(AppDbContext dbContext)
    {
        dbContext.Stores.AddRange(Store1, Store2);
        await dbContext.SaveChangesAsync();
    }
}
