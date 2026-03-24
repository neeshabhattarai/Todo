using FluentValidation;
using TodoApi.Applicaiton.Task.Command.CreateTask;
using Xunit;
namespace DefaultNamespace;

public class CreateTaskValidatorTest
{
   [Fact]
   public void CreateTask_ShouldReturnTrue()
   {
      var taskValidator = new CreateTaskValidation();
      var task = new CreateTask
      {
      }; 
      var result=taskValidator.Validate(task);
      foreach (var error in result.Errors)
      {
         Console.WriteLine(error.ErrorMessage);
      }
      Assert.NotEmpty(result.Errors);
      // Assert.Equal(result.IsValid, !result.Errors.Any());
   }
}