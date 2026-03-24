using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Domain.Constant;

namespace TodoApi.Controller;
[ApiController]
[Route("api/[controller]/[action]")]
public class TaskController:ControllerBase
{
    public TaskController()
    {
        
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
    
}