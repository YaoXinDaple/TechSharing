using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRPresentation.Plans.GetPlan
{
    public  record GetPlanResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string CreateUser { get; init; }
        public DateTime CreateDate { get; init; }
        public DateTime? ElapseStartDate { get; init; }
        public DateTime? ElapseEndDate { get; init; }
    }
}
