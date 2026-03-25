using MediatR;
using TodoApi.Applicaiton.DTO;
using TodoApi.Application.Task.PageReturn;

namespace TodoApi.Application.Task.Queries.GetAllTasks;

public class GetAllTaskCommand : IRequest<PageResult>
{
    public string? searchParams { get; set; }
    public int pageNumber { get; set; }
    public int pageSize { get; set; }
    public string? orderBy { get; set; }
    public string? sortDirection { get; set; }
}
