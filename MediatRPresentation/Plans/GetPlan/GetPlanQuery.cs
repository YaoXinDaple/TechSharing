using MediatR;

namespace MediatRPresentation.Plans.GetPlan
{
    public record GetPlanQuery(Guid Id) : IRequest<GetPlanResponse>;
}
