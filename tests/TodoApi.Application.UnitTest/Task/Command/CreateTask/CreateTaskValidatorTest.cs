using NUnit;
using TodoApi.Applicaiton.Task.Command.CreateTask;


namespace TodoApi.Application.UnitTest.Task.Command.CreateTask;
[TestFixture]
public class CreateTaskValidatorTest
{
    
    
    [Test]
    public async System.Threading.Tasks.Task CreateTask_WithEmptyBoy_ShouldThrowError()
    {
        CreateTaskValidation createTaskValidation = new();
        var result=createTaskValidation.Validate(new Applicaiton.Task.Command.CreateTask.CreateTask());
        Assert.True(result.Errors.Count>0);
        Assert.True(result.Errors[0].ErrorMessage.Contains("Name"));
        Assert.True(result.Errors[1].ErrorMessage.Contains("Description"));
    }
    [Test]
    public async System.Threading.Tasks.Task CreateTask_WithNullValue_ShouldThrowError()
    {
        CreateTaskValidation createTaskValidation = new();
        var result=createTaskValidation.Validate(new Applicaiton.Task.Command.CreateTask.CreateTask
        {
            Name = string.Empty,
            Description = string.Empty
        });
        Assert.True(result.Errors.Count>0);
        Assert.True(result.Errors[0].ErrorMessage.Contains("Name"));
        Assert.True(result.Errors[1].ErrorMessage.Contains("Name"));
        Assert.True(result.Errors[2].ErrorMessage.Contains("Description"));
    }
    
    [Test]
    public async System.Threading.Tasks.Task CreateTask_WithOneKeyAndValueOnly_ShouldThrowError()
    {
        CreateTaskValidation createTaskValidation = new();
        var result=createTaskValidation.Validate(new Applicaiton.Task.Command.CreateTask.CreateTask
        {
            Name = "test"
        });
        Assert.True(result.Errors.Count>0);
        Assert.True(result.Errors[0].ErrorMessage.Contains("Description"));
    }
    [Test]
    public async System.Threading.Tasks.Task CreateTask_WithValidInput_ShouldReturnOk()
    {
        CreateTaskValidation createTaskValidation = new();
        var result=createTaskValidation.Validate(new Applicaiton.Task.Command.CreateTask.CreateTask
        {
            Name = "test",
            Description = "test"
        });
        Assert.Null(result.Errors.Count());
    }
    
    
}