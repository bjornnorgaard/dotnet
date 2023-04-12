using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Exceptions;
using Todos.Database;
using Todos.Database.Models;

namespace Todos.Features.Todos;

public class UpdateTodo
{
    public class Command : IRequest<Result>
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required bool IsCompleted { get; set; }
        public required Guid TodoId { get; set; }
    }

    public class Result
    {
        public required TodoDto UpdatedTodo { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.TodoId).NotEmpty();

            RuleFor(c => c.Title).NotEmpty()
                .MinimumLength(TodoConstants.Title.MinLength)
                .MaximumLength(TodoConstants.Title.MaxLength);

            RuleFor(c => c.Description)
                .MaximumLength(TodoConstants.Description.MaxLength);
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
            var todo = await _todoContext.Todos.AsTracking()
                .Where(t => t.Id == request.TodoId)
                .FirstOrDefaultAsync(ct);

            if (todo == null) throw new PlatformException(PlatformError.TodoNotFound);

            todo = new Todo
            {
                Id = request.TodoId,
                Title = request.Title,
                Description = request.Description,
                Completed = request.IsCompleted
            };

            await _todoContext.SaveChangesAsync(ct);

            var mapped = todo.ToDto();
            var result = new Result { UpdatedTodo = mapped };
            return result;
        }
    }
}