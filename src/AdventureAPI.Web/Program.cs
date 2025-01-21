using AdventureAPI.Web.Configurations;
using FastEndpoints.Security;

var builder = WebApplication.CreateBuilder(args);

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting web host");

builder.AddLoggerConfigs();

var appLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<Program>();

builder.Services.AddOptionConfigs(builder.Configuration, appLogger, builder);
builder.Services.AddServiceConfigs(appLogger, builder);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services
    .AddAuthenticationJwtBearer(
        s =>
        {
            s.SigningKey = jwtSettings["SigningKey"];
        },
        b =>
        {
            b.TokenValidationParameters.ValidIssuer = jwtSettings["Issuer"];
            b.TokenValidationParameters.ValidAudience = jwtSettings["Audience"];
        })
    .AddAuthorization()
    .AddFastEndpoints()
    .SwaggerDocument(o => { o.ShortSchemaNames = true; });

var app = builder.Build();

app.UseAppMiddleware();

app.Run();

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program
{
}
