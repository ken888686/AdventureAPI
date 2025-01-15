using AdventureAPI.Infrastructure.Data;
using AdventureAPI.Infrastructure.Services;
using Testcontainers.PostgreSql;

namespace AdventureAPI.IntegrationTests;

public class BaseEfRepoTestFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder().Build();
    public required AppDbContext DbContext;

    public async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();
        var connectionString = _postgresContainer.GetConnectionString();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        var fakeEventDispatcher = Substitute.For<IDomainEventDispatcher>();
        DbContext = new AppDbContext(options, fakeEventDispatcher);
        await DbContext.Database.EnsureCreatedAsync();
        var passwordService = new PasswordService();
        await SeedData.InitializeAsync(DbContext, passwordService);
    }

    public async Task DisposeAsync()
    {
        await DbContext.DisposeAsync();
        await _postgresContainer.StopAsync();
        await _postgresContainer.DisposeAsync();
    }
}
