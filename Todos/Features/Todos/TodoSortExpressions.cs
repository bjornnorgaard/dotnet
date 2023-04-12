using System.Linq.Expressions;
using Humanizer;
using Todos.Database.Models;

namespace Todos.Features.Todos;

public static class TodoSortExpressions
{
    public static Expression<Func<Todo, object>> Get(string propertyName)
    {
        return propertyName?.Pascalize() switch
        {
            nameof(TodoDto.Id) => todo => todo.Id,
            nameof(TodoDto.Title) => todo => todo.Title,
            nameof(TodoDto.Description) => todo => todo.Description,
            nameof(TodoDto.IsCompleted) => todo => todo.IsCompleted,
            _ => todo => todo.Id
        };
    }
}
