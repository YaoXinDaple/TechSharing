using FluentValidation;
using FluentValidationDemo.Repository;

namespace FluentValidationDemo.Models
{
    public class AccountSetNameValidator : AbstractValidator<Company>
    {
        private readonly DataRepository _dataRepository;
        public AccountSetNameValidator(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;

            RuleFor(x => x).MustAsync(async (x, t) => await _dataRepository.IsRegisteredCompanyName(x.Name)).WithMessage("企业名称必须是已注册的公司名");
        }
    }
}
