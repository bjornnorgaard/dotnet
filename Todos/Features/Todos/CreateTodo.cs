using FluentValidation;
using MediatR;
using Todos.Database;
using Todos.Database.Models;

namespace Todos.Features.Todos;

public class CreateTodo
{
    public class Command : IRequest<Result>
    {
        public required string Title { get; init; }
        public required string Description { get; init; }
    }

    public class Result
    {
        public Guid CreatedId { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
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
            var todo = new Todo
            {
                Description = request.Description,
                Title = request.Title
            };

            await _todoContext.Todos.AddAsync(todo, ct);
            await _todoContext.SaveChangesAsync(ct);

            var result = new Result { CreatedId = todo.Id };
            return result;
        }
    }
}
