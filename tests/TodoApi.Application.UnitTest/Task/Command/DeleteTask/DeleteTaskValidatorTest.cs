using Moq;
using TodoApi.Application.Task.Command.DeleteTask;

namespace TodoApi.Application.UnitTest.Task.Command.DeleteTask;
[TestFixture]
public class DeleteTaskValidatorTest
{
    [Test]
    public async System.Threading.Tasks.Task DeteTask_WithInvalidId_ThrowsErrors()
    {
        DeleteTaskValidation validator = new DeleteTaskValidation();
        var result=validator.Validate(new DeleteTaskCommand(0));
        Assert.IsFalse(result.IsValid);
        Assert.Greater(result.Errors.Count, 0);
        Assert.That(result.Errors[0].ErrorMessage,Is.EqualTo("TaskId is required"));
    }
    [Test]
    public async System.Threading.Tasks.Task DeteTask_Id_ReturnSuccess()
    {
        DeleteTaskValidation validator = new DeleteTaskValidation();
        var result=validator.Validate(new DeleteTaskCommand(10));
        Assert.IsTrue(result.IsValid);
        Assert.AreEqual(0,result.Errors.Count);
        
    }
    
    [Test]
    public async System.Threading.Tasks.Task DeteTask_WithNegativeId_ThrowsException()
    {
        DeleteTaskValidation validator = new DeleteTaskValidation();
        var result=validator.Validate(new DeleteTaskCommand(-10));
        Assert.IsFalse(result.IsValid);
        Assert.GreaterOrEqual(result.Errors.Count,1);
        Assert.That(result.Errors[0].ErrorMessage,Is.EqualTo("TaskId must be greater than or equal to 1"));
        
        
    }
    
}