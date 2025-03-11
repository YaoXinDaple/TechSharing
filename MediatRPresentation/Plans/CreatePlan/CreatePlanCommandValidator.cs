using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRPresentation.Plans.CreatePlan
{
    public class CreatePlanCommandValidator:AbstractValidator<CreatePlanCommand>
    {
        public CreatePlanCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.CreateUser).NotEmpty().WithMessage("CreateUser is required");
        }
    }
}
