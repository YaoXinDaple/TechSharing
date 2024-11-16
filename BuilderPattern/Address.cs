using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    public class Address
    {
        internal Address()
        {
        }
        public string Street { get; init; }
        public string Detail { get; init; }
        public string Province { get; init; }
        public string ZipCode { get; init; }
    }
}
