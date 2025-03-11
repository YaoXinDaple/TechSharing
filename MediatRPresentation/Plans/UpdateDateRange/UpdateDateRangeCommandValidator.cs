using FluentValidation;

namespace MediatRPresentation.Plans.UpdateDateRange
{
    public class UpdateDateRangeCommandValidator : AbstractValidator<UpdateDateRangeCommand>
    {
        public UpdateDateRangeCommandValidator()
        {
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate);
        }
    }
}
