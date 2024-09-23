using FluentValidation;

namespace FluentValidationDemo.Models
{
    public class AsSettingValidator : AbstractValidator<AsSetting>
    {
        public AsSettingValidator()
        {
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}
