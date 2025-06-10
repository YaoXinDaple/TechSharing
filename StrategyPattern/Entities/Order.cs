using StrategyPattern.Strategies;

namespace StrategyPattern.Entities
{
    internal class Order
    {
        internal Order(IDiscountStrategy discountStrategy)
        {
            DiscountStrategy = discountStrategy;
        }
        public decimal Amount { get; set; }
        public decimal DiscountRate { get; set; }
        public IDiscountStrategy DiscountStrategy { get; set; }

        public decimal GetFinalPrice()
        {
            if (DiscountStrategy == null)
                return Amount;

            // 方案1：如果策略仅依赖 amount
            // return DiscountStrategy.ApplyDiscount(Amount);

            // 方案2：如果策略需要多个属性（通过上下文）
            var context = new DiscountContext
            {
                Amount = this.Amount,
                DiscountRate = this.DiscountRate
            };
            return DiscountStrategy.ApplyDiscount(context); // 传递上下文，而非 Order
        }
    }
}
