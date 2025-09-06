using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern.Strategies
{
    public class FixedDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _fixedDiscount;

        public FixedDiscountStrategy(decimal fixedDiscount)
        {
            _fixedDiscount = fixedDiscount;
        }

        public decimal ApplyDiscount(decimal amount)
        {
            return amount - _fixedDiscount;
        }

        public decimal ApplyDiscount(DiscountContext context)
        {
            throw new NotImplementedException();
        }
    }
}
