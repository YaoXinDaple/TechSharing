using MediatRInfrastructure.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MediatRDomain.Plans
{
    public interface IPlanRepository
    {
        Task<Guid> CreateAsync(MediatRInfrastructure.Plans.Plan plan);

        Task<Plan> GetAsync(Guid id);

        Task UpdateAsync(Plan plan);
    }
}
