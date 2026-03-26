using AutoMapper;
using MediatR;
using TodoApi.Application.User;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Repository;

namespace TodoApi.Applicaiton.Task.Command.CreateTask;

public class CreateTaskHandler(ITask task,IMapper mapper,IUserContext userContext):IRequestHandler<CreateTask,int>
{
    public async Task<int> Handle(CreateTask request, CancellationToken cancellationToken)
    {
        
      var createTaskmapper=mapper.Map<Tasks>(request);
      var UserContext = userContext.GetCurrentUser();
      if (UserContext == null)
      {
          throw new InvalidOperationException("UserContext is null");
      }
      createTaskmapper.UserId = UserContext.Id;
      var createTask=await task.CreateTask(createTaskmapper);
      return createTask;
    }
}