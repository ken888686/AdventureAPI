﻿using System.Reflection;
using AdventureAPI.Core.Aggregates.CityAggregate;
using AdventureAPI.Core.Aggregates.StoreAggregate;
using AdventureAPI.Core.Aggregates.UserAggregate;
using AdventureAPI.UseCases.Auth.Login;
using AdventureAPI.UseCases.Auth.Register;
using AdventureAPI.UseCases.Cities.List;
using AdventureAPI.UseCases.Stores.Create;
using AdventureAPI.UseCases.Stores.Get;
using AdventureAPI.UseCases.Stores.List;
using AdventureAPI.UseCases.Users.Get;
using Ardalis.SharedKernel;

namespace AdventureAPI.Web.Configurations;

public static class MediatrConfigs
{
    public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
    {
        var mediatRAssemblies = new[]
        {
            // Core
            Assembly.GetAssembly(typeof(City)),
            Assembly.GetAssembly(typeof(Store)),
            Assembly.GetAssembly(typeof(User)),
            // UseCases
            // Auth
            Assembly.GetAssembly(typeof(LoginCommand)),
            Assembly.GetAssembly(typeof(RegisterCommand)),
            // City
            Assembly.GetAssembly(typeof(ListCitiesQuery)),
            // Store
            Assembly.GetAssembly(typeof(CreateStoreCommand)),
            Assembly.GetAssembly(typeof(GetStoreQuery)),
            Assembly.GetAssembly(typeof(ListStoresQuery)),
            // Users
            Assembly.GetAssembly(typeof(GetUserInfoQuery))
        };

        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
            .AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

        return services;
    }
}
