using Microsoft.OpenApi.Models;
using Todos.Options;

namespace Todos.Configuration;

internal static class SwaggerConfiguration
{
    internal static void AddPlatformSwagger(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var options = new SwaggerOption(configuration);
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(t =>
            {
                if (string.IsNullOrWhiteSpace(t.FullName))
                {
                    return t.Name;
                }

                if (t.FullName.Contains('+'))
                {
                    return t.FullName.Split(".").Last().Replace("+", "");
                }

                return t.Name;
            });
            c.SwaggerDoc("v1", new OpenApiInfo { Title = options.ApplicationTitle, Version = "v1" });
        });
    }

    internal static void UsePlatformSwagger(this IApplicationBuilder app, IConfiguration configuration)
    {
        var options = new SwaggerOption(configuration);

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            // OpenAPI URL: http://localhost:5001/swagger/v1/swagger.json
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{options.ApplicationTitle} v1");
        });
    }
}
