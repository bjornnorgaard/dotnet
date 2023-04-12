using System.Text.Json.Serialization;
using Ant.Platform.Configurations;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Todos.Configuration;
using Todos.Database;
using Todos.Filters;
using Todos.Options;
using Todos.PipelineBehaviors;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPlatformLogging(builder.Configuration);
builder.Host.UseSerilog();
builder.Services.AddControllers(o => o.Filters.Add<ExceptionFilter>()).AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddHealthChecks();
builder.Services.AddPlatformSwagger(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

var assembly = typeof(Todos.Program).Assembly;

var databaseOption = new DatabaseOption(builder.Configuration);
builder.Services.AddDbContext<TodoContext>(c => c.UseNpgsql(databaseOption.ConnectionString));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Todos.Program>());

// Order of pipeline-behaviors is important
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipeline<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

// Add validators
var validators = AssemblyScanner.FindValidatorsInAssemblies(new[] { assembly });
validators.ForEach(validator => builder.Services.AddTransient(validator.InterfaceType, validator.ValidatorType));


var app = builder.Build();

app.UsePlatformLogging(app.Configuration);
app.UsePlatformSwagger(app.Configuration);
app.MapControllers();
app.UseHealthChecks("/hc");

var scope = app.Services.CreateScope();
var database = scope.ServiceProvider.GetService<TodoContext>();
database.Database.Migrate();

app.Run();

namespace Todos
{
    public partial class Program
    {
    }
}
