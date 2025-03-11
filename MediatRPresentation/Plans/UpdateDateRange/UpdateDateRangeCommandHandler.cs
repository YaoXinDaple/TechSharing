using MediatR;
using MediatRDomain.Plans;
using MediatRInfrastructure.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRPresentation.Plans.UpdateDateRange
{
    public class UpdateDateRangeCommandHandler : IRequestHandler<UpdateDateRangeCommand>
    {
        private readonly IPlanRepository _planRepository;

        public UpdateDateRangeCommandHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task Handle(UpdateDateRangeCommand request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetAsync(request.PlanId);
            if (request.StartDate is null || request.EndDate is null)
            {
                plan.SetElapse(null);
            }
            else
            {
                DateRange dateRange = new DateRange(request.StartDate.Value, request.EndDate.Value, null);
                plan.SetElapse(dateRange);
            }
            await _planRepository.UpdateAsync(plan);
        }
    }
}
