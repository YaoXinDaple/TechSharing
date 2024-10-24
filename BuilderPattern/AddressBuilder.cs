using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    public  class AddressBuilder
    {
        public static AddressBuilder Empty() => new();
        private string _street;
        private string _detail;
        private string _province;
        private string _zipCode;
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
                Street = _street,
                Detail = _detail,
                Province = _province,
                ZipCode = _zipCode
            };
        }
    }
}
