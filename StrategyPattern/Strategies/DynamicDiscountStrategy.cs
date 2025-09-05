using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern.Strategies
{
    public class DynamicDiscountStrategy : IDiscountStrategy
    {
        public decimal ApplyDiscount(DiscountContext context)
        {
            if (context.Amount > 1000)
                return context.Amount * 0.8m; // 大额订单打 8 折
            else
                return context.Amount * (1 - context.DiscountRate); // 按折扣率计算
        }
    }
}
