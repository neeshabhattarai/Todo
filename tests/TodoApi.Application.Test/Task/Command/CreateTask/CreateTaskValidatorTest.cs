using FluentValidation;
using TodoApi.Applicaiton.Task.Command.CreateTask;
using Xunit;
namespace DefaultNamespace;

public class CreateTaskValidatorTest
{
   [Fact]
   public void CreateTask_ShouldReturn_Error()
   {
      var taskValidator = new CreateTaskValidation();
      var task = new CreateTask();
      var result=taskValidator.Validate(task);
      foreach (var error in result.Errors)
      {
         Console.WriteLine(error.ErrorMessage);
      }
      Assert.NotEmpty(result.Errors);
      Assert.True(result.Errors[0].ErrorMessage.Contains("Name"));
      // Assert.IsType<ValidationException>(result.Errors.First());
      // Assert.Equal(result.IsValid, !result.Errors.Any());
   }

   [Fact]
   public void CreateTask_ShouldReturn_Valid()
   {
      var taskValidator = new CreateTask
      {
         Name = "test",
         Description = "test"

      };
      var validationTask = new CreateTaskValidation();
      var result=validationTask.Validate(taskValidator);
      Assert.Empty(result.Errors);
      Assert.True(result.IsValid);
   }
}