using FluentValidation;

namespace TodoApi.Applicaiton.Task.Command.CreateTask;

public class CreateTaskValidation:AbstractValidator<CreateTask>
{
    public CreateTaskValidation()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").Length(3, 100)
            .WithMessage("Name must be greater than 3 and less than 100");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
    }
}