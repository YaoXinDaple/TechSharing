namespace BuilderPattern
{
    public class AddressBuilder
    {
        public static AddressBuilder Empty() => new();
        private string? _street;
        private string? _detail;
        private string? _province;
        private string? _zipCode;

        private AddressBuilder()
        {
        }
        public AddressBuilder WithStreet(string street)
        {
            _street = street;
            return this;
        }
        public AddressBuilder WithDetail(string detail)
        {
            _detail = detail;
            return this;
        }
        public AddressBuilder WithProvince(string province)
        {
            _province = province;
            return this;
        }
        public AddressBuilder WithZipCode(string zipCode)
        {
            _zipCode = zipCode;
            return this;
        }
        public Address Build()
        {
            return new Address
            {
                Street = _street ?? string.Empty,
                Detail = _detail ?? string.Empty,
                Province = _province ?? string.Empty,
                ZipCode = _zipCode ?? string.Empty
            };
        }
    }
}
