using Serilog;
using Todos.Options;

namespace Ant.Platform.Configurations;

public static class LoggingConfiguration
{
    public static void AddPlatformLogging(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        Console.WriteLine("Setting up platform logging...");

        var options = new LoggingOption(configuration);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.WithProperty("Application", options.ApplicationName)
            .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();

        Log.Information("Logger configured!");
    }

    public static void UsePlatformLogging(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseSerilogRequestLogging();
    }
}