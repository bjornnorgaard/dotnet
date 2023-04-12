using Microsoft.EntityFrameworkCore;
using Todos.Database.Models;

namespace Todos.Database;

public class TodoContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }

    public TodoContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoContext).Assembly);
    }
}
