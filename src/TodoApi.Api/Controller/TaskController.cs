using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Applicaiton.Task.Command.CreateTask;
using TodoApi.Domain.Constant;

namespace TodoApi.Controller;
[ApiController]
[Route("api/[controller]/[action]")]
public class TaskController:ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [Authorize(Policy = PolicyCollection.CheckAge)]
    [Authorize(Policy = PolicyCollection.CheckEmail)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    
    public IActionResult GetTask()
    {
        return Ok("All Tasks Done");
    }

    [HttpPost]
    [Authorize(Policy = PolicyCollection.CheckAge)]
    [Authorize(Policy = PolicyCollection.CheckEmail)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult CreateTask([FromBody] CreateTask task)
    {
        var result=_mediator.Send(task);
        return CreatedAtAction(nameof(GetTask), new { id = result }, task);
    }
}