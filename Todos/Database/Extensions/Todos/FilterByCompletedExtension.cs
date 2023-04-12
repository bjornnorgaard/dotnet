using Todos.Database.Models;

namespace Todos.Database.Extensions.Todos;

public static class FilterByCompletedExtension
{
    public static IQueryable<Todo> WhereCompleted(this IQueryable<Todo> q)
    {
        return q.Where(todo => todo.Completed == true);
    }
}