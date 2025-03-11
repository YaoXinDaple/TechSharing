namespace MediatRWebApi.Controllers.Plans
{
    public record UpdateDateRangeRequest(Guid PlanId,DateTime StartDate,DateTime EndDate);
}
