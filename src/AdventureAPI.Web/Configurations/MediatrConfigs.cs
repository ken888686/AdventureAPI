using System.Reflection;
using AdventureAPI.Core.Aggregates.StoreAggregate;
using AdventureAPI.UseCases.Stores.Create;
using AdventureAPI.UseCases.Stores.Get;
using Ardalis.SharedKernel;

namespace AdventureAPI.Web.Configurations;

public static class MediatrConfigs
{
    public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
    {
        var mediatRAssemblies = new[]
        {
            // Core
            Assembly.GetAssembly(typeof(Store)),
            // UseCases
            Assembly.GetAssembly(typeof(GetStoreQuery)),
            Assembly.GetAssembly(typeof(CreateStoreCommand))
        };

        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
            .AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

        return services;
    }
}
