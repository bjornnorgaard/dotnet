using Microsoft.EntityFrameworkCore;
using Todos;
using Todos.Database;
using Todos.Options;

var builder = PlatformExtensions.CreatePlatformBuilder(args);
var databaseOption = new DatabaseOption(builder.Configuration);
builder.Services.AddDbContext<TodoContext>(c => c.UseNpgsql(databaseOption.ConnectionString));

var app = builder.Build();
app.UsePlatformServices();
var scope = app.Services.CreateScope();
var database = scope.ServiceProvider.GetService<TodoContext>()!;
database.Database.Migrate();

app.Run();

public abstract partial class Program
{
}
