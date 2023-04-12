using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Platform.PipelineBehaviors;

namespace Platform.Configuration;

public static class MediatrConfiguration
{
    public static void AddPlatformMediatr(this WebApplicationBuilder builder, Assembly assembly)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // Order of pipeline-behaviors is important
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipeline<,>));
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

        // Add validators
        var validators = AssemblyScanner.FindValidatorsInAssemblies(new[] { assembly });
        validators.ForEach(validator =>
            builder.Services.AddTransient(validator.InterfaceType, validator.ValidatorType));
    }
}
