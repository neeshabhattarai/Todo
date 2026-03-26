using AutoMapper;
using Moq;
using TodoApi.Applicaiton.Task.Command.CreateTask;
using TodoApi.Application.User;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Repository;
using Xunit.Sdk;

namespace TodoApi.Applicaiton.Test.Task.Command.CreateTask;

public class CreateTaskHandlerTest
{
    [Fact]
    public async void CreateTask_ShouldReturnCreatedTask()
    {
        var taskMapper=new Mock<IMapper>();
        var taskI = new Mock<ITask>();
        var userContext=new Mock<IUserContext>();
        var currentUser = new CurrentUser
        (
             "abc@gmail.com",
             "10",
             "abc",
             "Admin"
             
        );
            userContext.Setup(x=>x.GetCurrentUser()).Returns(currentUser);
        taskMapper.Setup(x => x.Map<Tasks>(It.IsAny<Applicaiton.Task.Command.CreateTask.CreateTask>())).Returns(new Tasks());
        var task=new CreateTaskHandler(taskI.Object,taskMapper.Object,userContext.Object);
       var result=await task.Handle(It.IsAny<Applicaiton.Task.Command.CreateTask.CreateTask>(), CancellationToken.None);
        Assert.NotNull(result);
        
    }
}