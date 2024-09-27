using FluentValidation;

namespace FluentValidationDemo.Models
{
    public class AsSettingValidator : AbstractValidator<AsSetting>
    {
        private readonly IValidator<Company> _companyValidator;
        public AsSettingValidator(IValidator<Company> companyValidator)
        {
            _companyValidator = companyValidator;
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Company).SetValidator(_companyValidator);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}
