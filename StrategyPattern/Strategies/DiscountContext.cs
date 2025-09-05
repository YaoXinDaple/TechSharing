using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern.Strategies
{
    public class DiscountContext
    {
        public decimal Amount { get; set; }
        public decimal DiscountRate { get; set; }
    }
}
