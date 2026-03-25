using TodoApi.Application.Task.Command.DeleteTask;

namespace TestProject1.Task.Command.DeleteTask;

public class DeleteTaskValidationTest
{
    [Fact]
    public async void DeleteTask_ReturnTrue()
    {
        var task = new DeleteTaskCommand(-10);
        var taskHandler = new DeleteTaskValidation();
        var result=await taskHandler.ValidateAsync(task);
        Assert.NotEmpty(result.Errors);
        Assert.False(result.IsValid);
    }
}