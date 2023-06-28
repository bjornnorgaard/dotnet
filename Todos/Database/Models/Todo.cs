namespace Todos.Database.Models;

public class Todo
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public bool Completed { get; set; }
    public AppUser? CretedByUser { get; set; }
    public string? CretedByUserId { get; set; }
}
