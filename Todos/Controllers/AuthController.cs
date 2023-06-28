using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todos.Features.Auth;
using Todos.Features.Todos;

namespace Todos.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost(Routes.Auth.SignUp)]
    public async Task<SignUp.Result> SignUp(
        [FromBody] SignUp.Command command,
        CancellationToken ct)
    {
        return await _mediator.Send(command, ct);
    }

    //[AllowAnonymous]
    //[HttpPost(Routes.Auth.SignIn)]
    //public async Task<SignIn.Result> SignIn(
    //    [FromBody] SignIn.Command command,
    //    CancellationToken ct)
    //{
    //    return await _mediator.Send(command, ct);
    //}
}
