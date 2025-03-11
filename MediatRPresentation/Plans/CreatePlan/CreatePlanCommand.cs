using MediatR;

namespace MediatRPresentation.Plans.CreatePlan
{
    public record CreatePlanCommand(string Name, string CreateUser) : IRequest<Guid>;
}
