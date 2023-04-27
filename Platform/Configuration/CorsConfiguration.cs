using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Platform.Options;

namespace Platform.Configuration;

public static class CorsConfiguration
{
    private const string DefaultPolicy = "DefaultCorsPolicy";

    public static void AddPlatformCors(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var corsOptions = new CorsOptions(configuration);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(DefaultPolicy, policyBuilder =>
            {
                policyBuilder.WithOrigins(corsOptions.AllowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }

    public static void UsePlatformCors(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseCors(DefaultPolicy);
    }
}
