using FluentValidation;

namespace TodoApi.Application.Task.Queries.GetTaskById;

public class GetTaskByIdValidation:AbstractValidator<GetTaskByIdCommand>
{
    public GetTaskByIdValidation()
    {
        RuleFor(x => x.TaskId).NotEmpty().WithMessage("TaskId is required.").GreaterThanOrEqualTo(0).WithMessage("TaskId must be positive.");
    }
}