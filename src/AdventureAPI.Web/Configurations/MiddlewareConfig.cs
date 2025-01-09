using Ardalis.ListStartupServices;

namespace AdventureAPI.Web.Configurations;

public static class MiddlewareConfig
{
    public static IApplicationBuilder UseAppMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseShowAllServicesMiddleware();
        }
        else
        {
            // FastEndpoints
            app.UseDefaultExceptionHandler();
            app.UseHsts();
        }

        app.UseFastEndpoints()
            .UseSwaggerGen();

        // Note this will drop Authorization headers
        // app.UseHttpsRedirection();

        return app;
    }
}
