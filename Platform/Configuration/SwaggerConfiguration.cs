using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Platform.Options;

namespace Platform.Configuration;

internal static class SwaggerConfiguration
{
    internal static void AddPlatformSwagger(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var options = new SwaggerOptions(configuration);

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
            c.SwaggerDoc("v1", new OpenApiInfo { Title = options.ApplicationName, Version = "v1" });
        });
    }

    internal static void UsePlatformSwagger(this IApplicationBuilder app, IConfiguration configuration)
    {
        var options = new SwaggerOptions(configuration);

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            // OpenAPI URL: http://localhost:5000/swagger/v1/swagger.json
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{options.ApplicationName} v1");
        });
    }
}
