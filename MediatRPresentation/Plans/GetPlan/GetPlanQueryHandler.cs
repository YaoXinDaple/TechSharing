using MediatR;
using MediatRDomain.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRPresentation.Plans.GetPlan
{
    public class GetPlanQueryHandler : IRequestHandler<GetPlanQuery, GetPlanResponse>
    {
        private readonly IPlanRepository _planRepository;

        public GetPlanQueryHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<GetPlanResponse> Handle(GetPlanQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("Invalid Plan Id");
            }
            var plan = await _planRepository.GetAsync(request.Id);
            return new GetPlanResponse
            {
                Id = plan.Id,
                Name = plan.Name,
                CreateDate = plan.CreationTime,
                CreateUser = plan.CreateUser,
                ElapseStartDate = plan.Elapse?.Start,
                ElapseEndDate = plan.Elapse?.End,
            };
        }
    }
}
