using MediatR;

namespace TodoApi.Applicaiton.Task.Command.CreateTask;

public class CreateTaskHandler:IRequestHandler<CreateTask>
{
    public System.Threading.Tasks.Task Handle(CreateTask request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}