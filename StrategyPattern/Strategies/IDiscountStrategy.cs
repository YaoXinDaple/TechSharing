using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern.Strategies
{
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(DiscountContext context); // 接收上下文，而非直接依赖 Order
    }
}
