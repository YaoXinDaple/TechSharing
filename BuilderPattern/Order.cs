using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    public class Order
    {
        internal Order()
        {
        }
        public int Number { get; init; }
        public DateTime CreatedOn { get; init; }

        public Address ShippingAddress { get; init; }
    }
}
