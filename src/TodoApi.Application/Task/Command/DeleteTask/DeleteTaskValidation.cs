using FluentValidation;

namespace TodoApi.Application.Task.Command.DeleteTask;

public class DeleteTaskValidation:AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskValidation()
    {
        RuleFor(command => command.TaskId).NotEmpty().WithMessage("TaskId is required").GreaterThanOrEqualTo(1).WithMessage("TaskId must be greater than or equal to 1");
    }
}