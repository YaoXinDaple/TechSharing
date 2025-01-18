namespace BuilderPattern
{
    /// <summary>
    /// 按步骤构建对象
    /// </summary>
    public class AddressGuidedBuilder
    {
        public static AddressGuidedBuilder Create() => new AddressGuidedBuilder();

        private AddressGuidedBuilder()
        {
        }

        internal AddressBuilderStepOne WithZipCode(string zipCode)
        {
            return new AddressBuilderStepOne(zipCode);
        }
        internal class AddressBuilderStepOne
        {
            private string _zipCode { get; }

            internal AddressBuilderStepOne(string zipCode)
            {
                _zipCode = zipCode;
            }

            public AddressBuilderStepTwo WithProvince(string province)
            {
                return new AddressBuilderStepTwo(province, _zipCode);
            }
        }

        internal class AddressBuilderStepTwo
        {
            private string _province { get; }
            private string _zipCode { get; }
            internal AddressBuilderStepTwo(string zipCode, string province)
            {
                _zipCode = zipCode;
                _province = province;
            }

            public AddressBuilderStepThree WithStreetAndDetail(string street, string detail)
            {
                return new AddressBuilderStepThree(_zipCode, _province, street, detail);
            }

        }

        internal class AddressBuilderStepThree
        {
            private string _zipCode { get; }
            private string _province { get; }
            private string _street { get; }
            private string _detail { get; }

            internal AddressBuilderStepThree(string zipCode, string province, string street, string detail)
            {
                _zipCode = zipCode;
                _province = province;
                _street = street;
                _detail = detail;
            }

            internal Address Build()
            {
                return new Address
                {
                    Street = _street,
                    Detail = _detail,
                    Province = _province,
                    ZipCode = _zipCode
                };
            }
        }
    }
}

