using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRInfrastructure.Plans
{
    public class Plan
    {
        private Plan()
        {
        }
        public Plan(Guid id, string name, DateTime creationTime, string createUser)
        {
            Id = id;
            Name = name;
            CreationTime = creationTime;
            CreateUser = createUser;
        }

        public void SetElapse(DateRange? elapse)
        {
            if (elapse == null)
            {
                Elapse = null;
            }
            else
            {
                Elapse = elapse;
            }
        }

        public void SetCompleteTime(DateTime completeTime)
        {
            CompleteTime = completeTime;
        }

        public Guid Id { get; init; }
        public string Name { get; private set; }

        public DateRange? Elapse { get; private set; }

        public DateTime? CompleteTime { get; private set; }

        public DateTime CreationTime { get; init; }

        public string CreateUser { get; init; }
    }
}
