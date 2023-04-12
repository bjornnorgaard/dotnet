using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todos.Database;
using Todos.Database.Extensions;

namespace Todos.Features.Todos;

public class GetTodos
{
    public class Command : IRequest<Result>
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string SortProperty { get; set; } = nameof(TodoDto.Id);
        public SortOrder SortOrder { get; set; } = SortOrder.None;
    }

    public class Result
    {
        public required List<TodoDto> Todos { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.PageNumber)
                .GreaterThanOrEqualTo(0);

            RuleFor(c => c.PageSize).NotEmpty()
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);

            RuleFor(c => c.SortOrder).IsInEnum();
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
            var todos = await _todoContext.Todos.AsNoTracking()
                .SortBy(TodoSortExpressions.Get(request.SortProperty), request.SortOrder)
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(ct);

            var mapped = todos.ToDto();
            var result = new Result { Todos = mapped };

            return result;
        }
    }
}
