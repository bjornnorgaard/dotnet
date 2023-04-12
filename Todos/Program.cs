using Microsoft.EntityFrameworkCore;
using Platform;
using Todos.Database;
using Todos.Options;

var assembly = typeof(Todos.Program).Assembly;

var builder = PlatformExtensions.CreatePlatformBuilder(args, assembly);
var databaseOption = new DatabaseOption(builder.Configuration);
builder.Services.AddDbContext<TodoContext>(c => c.UseNpgsql(databaseOption.ConnectionString));

var app = builder.Build();
app.UsePlatformServices();
var scope = app.Services.CreateScope();
var database = scope.ServiceProvider.GetService<TodoContext>()!;
database.Database.Migrate();

app.Run();

namespace Todos
{
    public abstract partial class Program
    {
    }
}
