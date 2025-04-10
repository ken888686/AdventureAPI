using AdventureAPI.Core.Interfaces;
using AdventureAPI.Infrastructure.Data;
using AdventureAPI.Infrastructure.Services;

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

        // JWT Create
        var jwtSettings = config.GetSection("JwtSettings");
        var jwtSigningKey = Guard.Against.NullOrEmpty(jwtSettings["SigningKey"]);
        var jwtIssuer = Guard.Against.NullOrEmpty(jwtSettings["Issuer"]);
        var jwtAudience = Guard.Against.NullOrEmpty(jwtSettings["Audience"]);
        var jwtExpireDays = int.Parse(Guard.Against.NullOrEmpty(jwtSettings["ExpireDays"]));
        services.Configure<JwtCreationOptions>(
            options =>
            {
                options.SigningKey = jwtSigningKey;
                options.Issuer = jwtIssuer;
                options.Audience = jwtAudience;
                options.ExpireAt = DateTime.UtcNow.AddDays(jwtExpireDays);
            });

        services
            .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>))
            .AddScoped<IJwtTokenGenerator, JwtTokenGenerator>()
            .AddScoped<IPasswordService, PasswordService>();

        logger.LogInformation("{Project} services registered", "Infrastructure");

        return services;
    }
}
