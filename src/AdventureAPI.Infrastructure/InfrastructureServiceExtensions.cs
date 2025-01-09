using AdventureAPI.Infrastructure.Data;

namespace AdventureAPI.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger)
    {
        var connectionString = Guard.Against.NullOrEmpty(config.GetConnectionString("DefaultConnection"));
        services.AddApplicationDbContext(connectionString);

        services
            .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        logger.LogInformation("{Project} services registered", "Infrastructure");

        return services;
    }
}
