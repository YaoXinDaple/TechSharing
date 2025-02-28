using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxItemCategorySync.SyncCustomer
{
    public enum InSystemSyncTaskType
    {
        /// <summary>
        /// 从历史发票中同步客户信息
        /// </summary>
        SyncCustomerFromInvoiceHistory = 1,

        /// <summary>
        /// 从历史发票中同步项目信息
        /// </summary>
        SyncInvoiceItemFromInvoiceHistory = 2,
    }
}
