using MediatRDomain.Plans;

namespace MediatRInfrastructure.Plans
{
    public class PlanRepository : IPlanRepository
    {
        private readonly MediatRDbContext _dbContext;

        public PlanRepository(MediatRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateAsync(Plan plan)
        {

            _dbContext.Plans.Add(plan);
            await _dbContext.SaveChangesAsync();
            return plan.Id;
        }

        public async Task<Plan> GetAsync(Guid id)
        {
            var plan = await  _dbContext.Plans.FindAsync(id);
            if (plan == null) { 
             throw new KeyNotFoundException($"Plan with id {id} not found");
            }
            return plan;
        }

        public async Task UpdateAsync(Plan plan)
        {
            _dbContext.Plans.Update(plan);
            await _dbContext.SaveChangesAsync();
        }
    }
}
