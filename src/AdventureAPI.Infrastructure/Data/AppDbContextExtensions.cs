namespace AdventureAPI.Infrastructure.Data;

public static class AppDbContextExtensions
{
    public static void AddApplicationDbContext(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(
                connectionString,
                npgsqlOptions =>
                {
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null
                    );
                    npgsqlOptions.CommandTimeout(30);
                    npgsqlOptions.MaxBatchSize(100);
                    npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }
            );
            options.EnableSensitiveDataLogging(false);
            options.EnableDetailedErrors(false);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
}
