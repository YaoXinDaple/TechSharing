using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionOverInheritage.Invoices.Interfaces
{
    public interface IHasDetailBehavior
    {
        public string InvoiceCode { get; }
        public string InvoiceNumber { get; }
        public string DigitalInvoiceNumber { get; }
    }
}
