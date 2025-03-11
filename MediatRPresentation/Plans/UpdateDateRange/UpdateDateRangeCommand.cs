using MediatR;

namespace MediatRPresentation.Plans.UpdateDateRange
{
    public record UpdateDateRangeCommand(Guid PlanId, DateTime? StartDate, DateTime? EndDate) : IRequest;
}
