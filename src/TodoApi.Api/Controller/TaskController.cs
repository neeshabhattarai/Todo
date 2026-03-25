using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Applicaiton.Task.Command.CreateTask;
using TodoApi.Application.Task.Command.DeleteTask;
using TodoApi.Application.Task.Queries.GetAllTasks;
using TodoApi.Application.Task.Queries.GetTaskById;
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
    
    public async Task<IActionResult> GetTask([FromQuery]GetAllTaskCommand command)
    {
        var tasks= await _mediator.Send(command);
        return Ok(tasks);
    }

    [HttpPost]
    [Authorize(Policy = PolicyCollection.CheckAge)]
    [Authorize(Policy = PolicyCollection.CheckEmail)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateTask([FromBody] CreateTask task)
    {
        var result=await _mediator.Send(task);
        return CreatedAtAction(nameof(GetTask), new { id = result }, task);
    }

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTaskById([FromRoute] int id)
    {
        var task = await _mediator.Send(new GetTaskByIdCommand(id));
        return Ok(task);
    }

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteTaskById([FromQuery] DeleteTaskCommand deleteTaskCommand)
    {
        _mediator.Send(deleteTaskCommand);
        return NoContent();
    }
}