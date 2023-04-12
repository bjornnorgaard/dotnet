using Todos.Database.Models;

namespace Todos.Features.Todos;

public static class TodoMapper
{
    public static TodoDto ToDto(this Todo todo)
    {
        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            Completed = todo.Completed
        };
    }

    public static List<TodoDto> ToDto(this List<Todo> todos)
    {
        return todos.Select(t => t.ToDto()).ToList();
    }
}
