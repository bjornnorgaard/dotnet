using Microsoft.AspNetCore.Mvc;

namespace Todos.Controllers;

[ApiController]
public class TodosController : ControllerBase
{
    [HttpGet(Routes.Todos.GetTodo)]
    public ActionResult<string> GetTodo() => Ok();

    [HttpGet(Routes.Todos.GetTodos)]
    public ActionResult<string> GetTodos() => Ok();

    [HttpGet(Routes.Todos.CreateTodo)]
    public ActionResult<string> CreateTodo() => Ok();

    [HttpGet(Routes.Todos.UpdateTodo)]
    public ActionResult<string> UpdateTodo() => Ok();

    [HttpGet(Routes.Todos.DeleteTodo)]
    public ActionResult<string> DeleteTodo() => Ok();
}
