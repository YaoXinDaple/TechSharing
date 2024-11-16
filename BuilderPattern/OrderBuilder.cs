namespace BuilderPattern
{
    public class OrderBuilder
    {
        private int _number;
        private DateTime _createdOn;
        private AddressBuilder _addressBuilder = AddressBuilder.Empty();

        public static OrderBuilder Empty() => new();

        private OrderBuilder()
        {
        }

        public OrderBuilder WithNumber(int number)
        {
            _number = number;
            return this;
        }
        public OrderBuilder WithCreatedDateTime(DateTime createdOn)
        {
            _createdOn = createdOn;
            return this;
        }

        public OrderBuilder WithShippingAddress(Action<AddressBuilder> _action)
        {
            _action(_addressBuilder);
            return this;
        }
        public Order Build()
        {
            return new Order
            {
                Number = _number,
                CreatedOn = _createdOn,
                ShippingAddress = _addressBuilder.Build()
            };
        }
    }
}
