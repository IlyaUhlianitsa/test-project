using FluentValidation;

namespace Implicat.Models.UserTasks.Validators
{
    public class GetUserTasksRequestValidator : AbstractValidator<GetUserTasksRequest>
    {
        public GetUserTasksRequestValidator()
        {
            When(
                x => x.Statuses != null,
                () =>
                {
                    RuleFor(x => x.Statuses)
                        .NotEmpty()
                        .ForEach(x => x.IsInEnum())
                        .WithMessage("Status field is invalid");
                });

            RuleFor(x => x.DueDateFrom)
                .NotEmpty();

            RuleFor(x => x.DueDateTo)
                .NotEmpty()
                .GreaterThan(x => x.DueDateFrom);
        }
    }
}