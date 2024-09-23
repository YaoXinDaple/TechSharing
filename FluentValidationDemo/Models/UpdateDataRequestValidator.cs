using FluentValidation;
using FluentValidationDemo.Repository;

namespace FluentValidationDemo.Models
{
    public class UpdateDataRequestValidator : AbstractValidator<UpdateDataRequest>
    {
        private readonly DataRepository _dataRepository;

        public UpdateDataRequestValidator(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;

            RuleFor(x => x.AsId).MustAsync(async (x, t) =>
            {
                return BeAValidGuid(x) && await BeAExistsAsId(x);
            }).WithMessage("AsId不存在");

            RuleFor(x => x.AsName).NotEmpty();

            RuleFor(x => x.AsStartUseDate).NotEmpty();
        }

        private async Task<bool> BeAExistsAsId(string asId)
        {
            return await _dataRepository.CheckAsync(new Guid(asId));
        }

        private bool BeAValidGuid(string asId)
        {
            return Guid.TryParse(asId, out _);
        }
    }
}
