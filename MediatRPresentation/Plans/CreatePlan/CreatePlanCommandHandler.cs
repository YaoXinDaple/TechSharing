using MediatR;
using MediatRDomain.Plans;
using MediatRInfrastructure.Plans;

namespace MediatRPresentation.Plans.CreatePlan
{
    public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, Guid>
    {
        private readonly IPlanRepository _planRepository;

        public CreatePlanCommandHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }
        public async Task<Guid> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = new Plan(Guid.NewGuid(), request.Name, DateTime.Now, request.CreateUser);
            await _planRepository.CreateAsync(plan);
            return plan.Id;
        }
    }
}
