using MediatRInfrastructure.Plans;

namespace MediatRWebApi.Controllers.Plans
{
    public record CreatePlanRequest(string Name, string CreateUser);
}