using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Platform.Options;
using Prometheus;
using Serilog;

namespace Platform.Configuration;

public static class LoggingConfiguration
{
    public static void AddPlatformLogging(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        Console.WriteLine("Setting up platform logging...");

        var options = new LoggingOptions(configuration);

        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (env == null) env = "Development";
        
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.WithProperty("Application", options.ApplicationName)
            .Enrich.WithProperty("Environment", env)
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();

        Log.Information("Logger configured");
    }

    public static void UsePlatformLogging(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseHttpMetrics();
        app.UseMetricServer();
        app.UseSerilogRequestLogging();
    }
}
