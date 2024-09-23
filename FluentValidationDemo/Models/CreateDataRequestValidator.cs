using FluentValidation;

namespace FluentValidationDemo.Models
{
    public class CreateDataRequestValidator : AbstractValidator<CreateDataRequest>
    {
        public CreateDataRequestValidator()
        {
            RuleFor(x => x.AsId).Must(x=>BeAValidGuid(x)).WithMessage("AsId必须是有效的Guid值");
            RuleFor(x => x.AsName).NotEmpty();
            RuleFor(x => x.AsStartUseDate).Must(x=>NotAFutureDate(x)).WithMessage("账套启用时间不能大于当前年份")
                .DependentRules(() => {
                    RuleFor(x => x.AsStartUseDate).Must(x => x.Month == 1).WithMessage("账套启用时间必须是1月");
                });
            When(x => x.Settings.Count > 0, () =>
            {
                RuleForEach(x => x.Settings).SetValidator(new AsSettingValidator());
            });
        }

        private bool BeAValidGuid(string id)
        {
            if (Guid.TryParse(id, out Guid parsedId))
            { 
                return parsedId!= Guid.Empty;
            }
            return false;
        }

        private bool NotAFutureDate(DateOnly date)
        {
            return date.Year <= DateTime.Now.Year;
        }
    }
}
