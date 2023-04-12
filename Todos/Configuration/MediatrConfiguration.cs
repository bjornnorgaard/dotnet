using FluentValidation;
using MediatR;
using Todos.PipelineBehaviors;

namespace Todos.Configuration;

public static class MediatrConfiguration
{
    public static void AddPlatformMediatr(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

        // Order of pipeline-behaviors is important
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipeline<,>));
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

        // Add validators
        var validators = AssemblyScanner.FindValidatorsInAssemblies(new[] { typeof(Program).Assembly });
        validators.ForEach(validator =>
            builder.Services.AddTransient(validator.InterfaceType, validator.ValidatorType));
    }
}
