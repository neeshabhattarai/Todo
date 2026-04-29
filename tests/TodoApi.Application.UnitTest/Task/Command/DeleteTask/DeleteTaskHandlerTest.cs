using Moq;
using TodoApi.Application.Task.Command.DeleteTask;
using TodoApi.Application.User;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Exception;
using TodoApi.Domain.Repository;

namespace TodoApi.Application.UnitTest.Task.Command.DeleteTask;

public class DeleteTaskHandlerTest
{
    private DeleteTaskHandler _deleteTaskHandler;
    private Mock<ITask> _mockTask;
    private Mock<IUserContext> _mockUserContext;
    [SetUp]
    public void Setup()
    {
        _mockTask = new Mock<ITask>();
        _mockUserContext = new Mock<IUserContext>();
        _deleteTaskHandler = new(_mockTask.Object);
    }

    [Test]
    public async System.Threading.Tasks.Task DeleteTask_ValidTaskId_ShouldReturnSuccess()
    {
        var deleteTask = new Tasks
        {
            Id = 1,
            Name = "Task1"
        };
        _mockTask.Setup(x => x.DeleteTask(deleteTask));
        _mockTask.Setup(x=>x.GetTaskById(deleteTask.Id)).ReturnsAsync(deleteTask);
        var result=_deleteTaskHandler.Handle(new DeleteTaskCommand(deleteTask.Id), CancellationToken.None);
        _mockTask.Verify(x=>x.DeleteTask(deleteTask), Times.Once);
        _mockTask.Verify(x=>x.GetTaskById(deleteTask.Id), Times.Once);
        _mockTask.Verify(x=>x.DeleteTask(It.Is<Tasks>(y=>y.Id==deleteTask.Id)), Times.Once);
        Assert.True(result.IsCompletedSuccessfully);
    }
    [Test]
    public async System.Threading.Tasks.Task DeleteTask_InvalidId_ShouldThrowsException()
    {
        var deleteTask = new Tasks
        {
            Id = 1,
            Name = "Task1"
        };
        _mockTask.Setup(x => x.DeleteTask(deleteTask));
        _mockTask.Setup(x=>x.GetTaskById(deleteTask.Id)).ReturnsAsync((Tasks)null);
        var result=Assert.ThrowsAsync<NotFoundException>(async ()=>await _deleteTaskHandler.Handle(new DeleteTaskCommand(deleteTask.Id), CancellationToken.None));
        _mockTask.Verify(x=>x.DeleteTask(deleteTask), Times.Never);
        _mockTask.Verify(x=>x.GetTaskById(deleteTask.Id), Times.Once);
        Assert.That(result.Message,Does.Contain("Exception of type "));
    }
}