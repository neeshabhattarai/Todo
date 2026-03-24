using AutoMapper;
using MediatR;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Repository;

namespace TodoApi.Applicaiton.Task.Command.CreateTask;

public class CreateTaskHandler(ITask task,IMapper mapper):IRequestHandler<CreateTask,int>
{
    public async Task<int> Handle(CreateTask request, CancellationToken cancellationToken)
    {
        
      var createTask= await task.CreateTask(mapper.Map<Tasks>(request));
      return createTask;
    }
}