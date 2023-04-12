using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todos.Database;
using Todos.Exceptions;

namespace Todos.Features.Todos;

public class GetTodo
{
    public class Command : IRequest<Result>
    {
        public Guid TodoId { get; set; }
    }

    public class Result
    {
        public required TodoDto Todo { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.TodoId).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly TodoContext _todoContext;

        public Handler(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public async Task<Result> Handle(Command request, CancellationToken ct)
        {
            var todo = await _todoContext.Todos.AsNoTracking()
                .Where(t => t.Id == request.TodoId)
                .FirstOrDefaultAsync(ct);

            if (todo == null) throw new PlatformException(PlatformError.TodoNotFound);

            var mapped = todo.ToDto();
            var result = new Result { Todo = mapped };

            return result;
        }
    }
}
