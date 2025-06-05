using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionOverInheritage.Invoices.Interfaces
{
    public interface IInvoice:IHasDetailBehavior,IHasEntryBehavior,IHasBuyerInfomationBehavior,IHasSellerInformationBehavior
    {
    }
}
