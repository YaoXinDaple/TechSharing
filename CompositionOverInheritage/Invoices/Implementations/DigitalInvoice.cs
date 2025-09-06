using CompositionOverInheritage.Invoices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionOverInheritage.Invoices.Implementations
{
    public class DigitalInvoice : IInvoice, IHasDetailBehavior,IHasBuyerInfomationBehavior,IHasSellerInformationBehavior
    {
        string IHasDetailBehavior.InvoiceCode => throw new NotSupportedException();

        string IHasDetailBehavior.InvoiceNumber => throw new NotSupportedException();

        public string DigitalInvoiceNumber => throw new NotImplementedException();

        public string BuyerName => throw new NotImplementedException();

        public string BuyerTaxNumber => throw new NotImplementedException();

        public string SellerName => throw new NotImplementedException();

        public string SellerTaxNumber => throw new NotImplementedException();
    }
}
