using CompositionOverInheritage.Invoices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionOverInheritage.Invoices.Implementations
{
    public class RailwayInvoice:IInvoice
    {
        string IHasSellerInformationBehavior.SellerName => throw new NotSupportedException();
        string IHasSellerInformationBehavior.SellerTaxNumber => throw new NotSupportedException();
        public string BuyerName => throw new NotImplementedException();
        public string BuyerTaxNumber => throw new NotImplementedException();

        string IHasDetailBehavior.InvoiceCode => throw new NotSupportedException();

        string IHasDetailBehavior.InvoiceNumber => throw new NotSupportedException();

        public string DigitalInvoiceNumber => throw new NotImplementedException();
    }
}
