using System.Text.Json.Serialization;
using Ant.Platform.Configurations;
using Todos.Configuration;
using Todos.Filters;

namespace Todos;

public static class PlatformExtensions
{
    public static WebApplicationBuilder CreatePlatformBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.AddPlatformLogging(builder.Configuration);
        builder.AddPlatformSwagger(builder.Configuration);
        builder.AddPlatformMediatr();
        
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
        app.UseHealthChecks("/hc");
    }
}
