namespace Todos.Features.Todos;

public class TodoDto
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required bool Completed { get; set; }
    public string? CretedByUserId { get; set; }
}
