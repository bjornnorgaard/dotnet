using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todos.Features.Todos;

namespace Todos.Controllers;

[ApiController]
public class TodosController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Routes.Todos.GetTodo)]
    public async Task<GetTodo.Result> GetTodo(
        [FromBody] GetTodo.Command command,
        CancellationToken ct)
    {
        return await _mediator.Send(command, ct);
    }

    [HttpPost(Routes.Todos.GetTodos)]
    public async Task<GetTodos.Result> GetTodos(
        [FromBody] GetTodos.Command command,
        CancellationToken ct)
    {
        return await _mediator.Send(command, ct);
    }

    [HttpPost(Routes.Todos.CreateTodo)]
    public async Task<CreateTodo.Result> CreateTodo(
        [FromBody] CreateTodo.Command command,
        CancellationToken ct)
    {
        return await _mediator.Send(command, ct);
    }

    [HttpPost(Routes.Todos.UpdateTodo)]
    public async Task<UpdateTodo.Result> UpdateTodo(
        [FromBody] UpdateTodo.Command command,
        CancellationToken ct)
    {
        return await _mediator.Send(command, ct);
    }

    [HttpPost(Routes.Todos.DeleteTodo)]
    public AcceptedResult DeleteTodo(
        [FromBody] DeleteTodo.Command command,
        CancellationToken ct)
    {
        _mediator.Send(command, ct);
        return Accepted();
    }
}
