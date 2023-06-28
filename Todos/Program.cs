using Microsoft.EntityFrameworkCore;
using Platform;
using Platform.Configuration;
using Todos.Database;
using Todos.Features.Auth;
using Todos.Options;

var assembly = typeof(Todos.Program).Assembly;

var builder = PlatformExtensions.CreatePlatformBuilder(args, assembly);
builder.AddIdentityClaimAuth(builder.Configuration);
var databaseOptions = new DatabaseOptions(builder.Configuration);
builder.Services.AddDbContext<TodoContext>(c => c.UseNpgsql(databaseOptions.ConnectionString));
builder.Services.AddScoped<JwtService>();

var app = builder.Build();
app.UsePlatformServices();
app.UseIdentityClaimAuth();
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
