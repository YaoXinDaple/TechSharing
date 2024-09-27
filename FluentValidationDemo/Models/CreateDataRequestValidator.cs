using FluentValidation;

namespace FluentValidationDemo.Models
{
    public class CreateDataRequestValidator : AbstractValidator<CreateDataRequest>
    {
        private readonly IValidator<AsSetting> _asSettingValidator;
        public CreateDataRequestValidator(IValidator<AsSetting> asSettingValidator)
        {
            _asSettingValidator = asSettingValidator;
            RuleFor(x => x.AsId).Must(BeAValidGuid).WithMessage("AsId必须是有效的Guid值");
            RuleFor(x => x.AsName).NotEmpty();

            RuleFor(x => x.AsStartUseDate).Must(NotAFutureDate).WithMessage("账套启用时间不能大于当前年份")
                .DependentRules(() =>
                {
                    RuleFor(x => x.AsStartUseDate).Must(x => x.Month == 1).WithMessage("账套启用时间必须是1月");
                });

            When(x => x.Settings.Count > 0, () =>
            {
                RuleForEach(x => x.Settings).SetValidator(_asSettingValidator);
            });
        }

        private bool BeAValidGuid(string id)
        {
            if (Guid.TryParse(id, out Guid parsedId))
            {
                return parsedId != Guid.Empty;
            }
            return false;
        }

        private bool NotAFutureDate(DateOnly date)
        {
            return date.Year <= DateTime.Now.Year;
        }
    }
}
