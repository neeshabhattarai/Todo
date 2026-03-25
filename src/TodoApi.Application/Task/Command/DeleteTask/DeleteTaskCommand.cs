using MediatR;

namespace TodoApi.Application.Task.Command.DeleteTask;

public class DeleteTaskCommand(int id):IRequest
{
    public int TaskId { get; set; } = id;
}