using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Platform.Configuration;
using Platform.Filters;
using Platform.Middleware;
using Serilog;

namespace Platform;

public static class PlatformExtensions
{
    public static WebApplicationBuilder CreatePlatformBuilder(string[] args, Assembly assembly)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddPlatformLogging(builder.Configuration);
        builder.AddPlatformSwagger(builder.Configuration);
        builder.AddPlatformMediatr(assembly);

        builder.Services.AddHealthChecks();
        builder.Services.AddControllers(o => o.Filters.Add<ExceptionFilter>()).AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return builder;
    }

    public static void UsePlatformServices(this WebApplication app)
    {
        app.UsePlatformLogging(app.Configuration);
        app.UsePlatformSwagger(app.Configuration);
        app.MapControllers();
        app.UseMiddleware<CorrelationMiddleware>();
        app.UseHealthChecks("/hc");

        Log.Information("Successfully started platform");
    }
}
