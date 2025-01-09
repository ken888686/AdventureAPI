using AdventureAPI.Infrastructure;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace AdventureAPI.Web.Configurations;

public static class ServiceConfigs
{
    public static IServiceCollection AddServiceConfigs(this IServiceCollection services,
        ILogger logger,
        WebApplicationBuilder builder)
    {
        services
            .AddInfrastructureServices(builder.Configuration, logger)
            .AddMediatrConfigs();

        // if (builder.Environment.IsDevelopment())
        // {
        // }

        logger.LogInformation("{Project} services registered", "Mediatr");

        return services;
    }
}
