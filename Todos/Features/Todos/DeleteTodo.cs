using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todos.Database;
using Todos.Exceptions;

namespace Todos.Features.Todos;

public class DeleteTodo
{
    public class Command : IRequest
    {
        public Guid TodoId { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.TodoId).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly TodoContext _todoContext;

        public Handler(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }
        
         public async Task Handle(Command request, CancellationToken ct)
         {
             var todo = await _todoContext.Todos.AsTracking()
                 .Where(t => t.Id == request.TodoId)
                 .FirstOrDefaultAsync(ct);

             if (todo == null) throw new PlatformException(PlatformError.TodoNotFound);

             _todoContext.Todos.Remove(todo);
             await _todoContext.SaveChangesAsync(ct);
         }
    }
}
