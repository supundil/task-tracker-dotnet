using FluentValidation;
using TaskTracker.Application.DTOs.Task;

namespace TaskTracker.Application.Validators
{
    public class CreateTaskRequestValidator
    : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Description)
                .MaximumLength(1000);

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow)
                .When(x => x.DueDate.HasValue);

            RuleFor(x => x.Status)
                .IsInEnum();
        }
    }
}
