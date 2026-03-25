using MediatR;
using TodoApi.Applicaiton.DTO;

namespace TodoApi.Application.Task.Queries.GetTaskById;

public class GetTaskByIdCommand(int id):IRequest<TaskDTO>
{
    public int TaskId { get; } = id;
}