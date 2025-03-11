using MediatR;
using MediatRPresentation.Plans.CreatePlan;
using MediatRPresentation.Plans.GetPlan;
using MediatRPresentation.Plans.UpdateDateRange;
using Microsoft.AspNetCore.Mvc;

namespace MediatRWebApi.Controllers.Plans
{
    [ApiController]
    [Route("[controller]")]
    public class PlanController : ControllerBase
    {
        private ISender _sender;
        public PlanController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync(string planName)
        {
            var command = new CreatePlanCommand(planName, "sa");
            var planId = await _sender.Send(command);
            return Created($"get/{planId}", planId);
        }

        [HttpPost]
        [Route("UpdateDateRange")]
        public async Task<IActionResult> UpdateDateRangeAsync(UpdateDateRangeRequest request)
        {
            var command = new UpdateDateRangeCommand(request.PlanId, request.StartDate, request.EndDate);
            await _sender.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<GetPlanResponse> GetAsync(Guid id)
        { 
            var query = new GetPlanQuery(id);
            return await _sender.Send(query);
        }
    }
}
