using System.Net;
using Moq;
using TodoApi.Application.Task.Command.DeleteTask;
using TodoApi.Domain.Repository;

namespace TestProject1.Task.Command.DeleteTask;

public class DeleteTaskHandlerTest
{
    public Mock<ITask> taskRepo;
    public DeleteTaskHandler handler;
    public DeleteTaskHandlerTest()
    {
        taskRepo = new Mock<ITask>();
        handler = new DeleteTaskHandler(taskRepo.Object);
    }

    [Fact]
    public async void DeleteTaskHandler_ShouldReturnTrue_WhenTaskIsDeleted()
    {
       
        var result=handler.Handle(It.IsAny<DeleteTaskCommand>(), CancellationToken.None);
        Assert.True(result.IsCompleted);
    }

    [Fact]
    public async void DeleteTaskHandler_ShouldReturnFalse_WhenTaskIsNotDeleted()
    {
        var result=handler.Handle(new DeleteTaskCommand(-10), CancellationToken.None);
        Assert.True(result.IsFaulted);
        Assert.False(result.IsCompletedSuccessfully);
        
    }
}