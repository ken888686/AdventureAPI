using AdventureAPI.Core.Aggregates.CityAggregate;
using AdventureAPI.Core.Aggregates.StoreAggregate;
using AdventureAPI.Core.Aggregates.UserAggregate;
using AdventureAPI.Core.Interfaces;

namespace AdventureAPI.Infrastructure.Data;

public static class SeedData
{
    public static readonly City City1 = new("Tokyo", "Aaron");
    public static readonly City City2 = new("Osaka", "Aaron");
    public static readonly Store Store1 = new("Store 1", "Aaron");
    public static readonly Store Store2 = new("Store 2", "Annabelle");

    public static async Task InitializeAsync(
        AppDbContext dbContext,
        IPasswordService passwordService)
    {
        await PopulateTestDataAsync(dbContext, passwordService);
    }

    public static async Task PopulateTestDataAsync(
        AppDbContext dbContext,
        IPasswordService passwordService)
    {
        // City
        dbContext.Cities.AddRange(City1, City2);

        // Store
        dbContext.Stores.AddRange(Store1, Store2);

        // User
        var passwordHash = passwordService.Hash("test", out var salt);
        var user1 = new User(
            "ken888686",
            "ken888686@gmail.com",
            passwordHash,
            salt,
            "Aaron",
            "Tu");
        var user2 = new User(
            "kan_annabelle",
            "kan_annabelle@yahoo.co.jp",
            passwordHash,
            salt,
            "Annabelle",
            "Kan");
        dbContext.Users.AddRange(user1, user2);

        // Save
        await dbContext.SaveChangesAsync();
    }
}
