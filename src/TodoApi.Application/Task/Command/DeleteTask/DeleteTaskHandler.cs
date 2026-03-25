using MediatR;
using TodoApi.Domain.Repository;

namespace TodoApi.Application.Task.Command.DeleteTask;

public class DeleteTaskHandler(ITask taskRepo):IRequestHandler<DeleteTaskCommand>
{
    public async System.Threading.Tasks.Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task=await taskRepo.GetTaskById(request.TaskId);
        if (task == null)
        {
            
        }
    }
}