using Microsoft.EntityFrameworkCore;
using System.Buffers;
using System.Linq.Expressions;
using System.Threading.Channels;
using TaxItemCategorySync.EntityFrameworkCore.LemonInvoiceSuiteDbcontext;

namespace TaxItemCategorySync.SyncCustomer
{
    public class InSystemCustomerSyncServiceStructed
    {
        /// <summary>
        /// 从历史发票中同步客户信息时，每次查询的发票数量
        /// </summary>
        private const int FetchArchivedInvoiceBatchSize = 500;

        /// <summary>
        /// 同步客户信息的Channel容量
        /// </summary>
        private const int ChannelCapacity = 50;

        /// <summary>
        /// 客户同步数据的对象缓存池
        /// </summary>
        private readonly ArrayPool<CustomerSyncItem> _customerSyncItemArrayPool = ArrayPool<CustomerSyncItem>.Shared;

        private readonly Channel<SaveSyncCustomerMessage> _customerSyncChannel;

        private readonly LemonInvoiceSuiteContext _dbContext;

        public InSystemCustomerSyncServiceStructed(LemonInvoiceSuiteContext dbcontext)
        {
            var option = new BoundedChannelOptions(50)
            {
                AllowSynchronousContinuations = false,
                SingleReader = true,
                SingleWriter = true,
                FullMode = BoundedChannelFullMode.Wait
            };
            _customerSyncChannel = Channel.CreateUnbounded<SaveSyncCustomerMessage>();
            _dbContext = dbcontext;
        }

        /// <summary>
        /// 同步任务入口
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SyncCustomerFromInvoiceHistoryAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var writer = WriteCustomersSyncMessageTask(_customerSyncChannel.Writer, companyId);
            var reader = ReadCustomersSyncMessageTask(_customerSyncChannel.Reader, companyId);

            await writer;

            _customerSyncChannel.Writer.Complete();
            await reader;
        }

        /// <summary>
        /// 查询历史发票中，是否存在需要同步的客户信息
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private async Task WriteCustomersSyncMessageTask(ChannelWriter<SaveSyncCustomerMessage> writer, Guid companyId)
        {

            //构建是否存在任何尚未同步的发票的查询表达式
            Expression<Func<ArchivedInvoice, bool>> searchExpression = x =>
                    x.CompanyId == companyId &&
                    x.IsAmountPositive &&
                    x.Direction == (int)InvoiceDirectionType.Outcome &&
                    x.IssueDate > DateTime.Now.AddYears(-2);


            //从新到旧轮询所有发票，如果发票的客户名称在客户信息中不存在，则将客户名称添加到待更新客户信息列表中
            var customerNamesToSync = new HashSet<CustomerSyncItem>();
            var archivedInvoiceQuery = _dbContext.LemonArchivedInvoices.Where(searchExpression).OrderByDescending(x => x.IssueDate);

            //从 archivedInvoiceQuery 中每隔 500 条记录取出一次，遍历每条记录，如果客户名称不在 InDatabaseCustomerNames 中，则添加到 customerNamesToSync 中
            int pageNumber = 0;
            int pageSize = FetchArchivedInvoiceBatchSize;
            bool shouldFetchNextBatch = true;



            while (shouldFetchNextBatch)
            {
                var archivedInvoiceInfos = await archivedInvoiceQuery
                    .Select(a => new
                    {
                        CustomerName = a.BuyerName,
                        CustomerUscic = a.BuyerUscic,
                        ArchivedInvoiceId = a.Id,
                        a.IssueDate
                    })
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                if (archivedInvoiceInfos.Count == 0)
                {
                    shouldFetchNextBatch = false;
                    continue;
                }


                foreach (var info in archivedInvoiceInfos)
                {
                    var customer = new CustomerSyncItem
                    {
                        Name = info.CustomerName,
                        Uscic = info.CustomerUscic
                    };
                    SaveSyncCustomerMessage writeMessage;
                    customerNamesToSync.Add(customer);

                    writeMessage = new SaveSyncCustomerMessage
                    {
                        CompanyId = companyId,
                        ArchivedInvoiceId = info.ArchivedInvoiceId,
                        Item = customer
                    };

                    await writer.WriteAsync(writeMessage);
                }

                pageNumber++;

                // 如果返回的数据数量少于 pageSize，说明没有更多的数据了
                if (archivedInvoiceInfos.Count < pageSize)
                {
                    shouldFetchNextBatch = false;
                }
            }
            await Task.CompletedTask;
        }


        /// <summary>
        /// 将待同步的客户信息保存到数据库
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task ReadCustomersSyncMessageTask(ChannelReader<SaveSyncCustomerMessage> reader, Guid companyId)
        {
            SaveSyncCustomerMessage? lastMessage = null;

            //从本批次更新数据中获取最大和最小的 历史发票ID
            var idStart = Guid.Empty;
            var idEnd = Guid.Empty;

            await foreach (var message in reader.ReadAllAsync())
            {
                //更新本次写入数据的最大和最小历史发票ID
                var invoiceId = message.ArchivedInvoiceId;
                if (idStart.IsNullOrEmpty() && idEnd.IsNullOrEmpty())
                {
                    idStart = idEnd = invoiceId;
                }
                else
                {
                    if (GuidExtensions.CompareSequentialGuids(invoiceId, idStart) < 0)
                    {
                        idStart = invoiceId;
                    }
                    else if (GuidExtensions.CompareSequentialGuids(invoiceId, idEnd) > 0)
                    {
                        idEnd = invoiceId;
                    }
                }

                lastMessage = message;
            }
            if (lastMessage is not null)
            {
                await SaveSyncCustomersWithUowAsync(companyId, idStart, idEnd);
            }
        }

        /// <summary>
        /// 以工作单元方式保存客户信息和同步标记
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="customers"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task SaveSyncCustomersWithUowAsync(Guid companyId, Guid idStart, Guid idEnd)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// 客户项，待同步客户信息
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Uscic"></param>
        private struct CustomerSyncItem
        {
            public string Name;
            public string? Uscic;
        }

        /// <summary>
        /// 用于在 Channel 传入数据的消息体
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <param name="Item"></param>
        private struct SaveSyncCustomerMessage
        {
            public Guid CompanyId;
            public Guid ArchivedInvoiceId;
            public CustomerSyncItem? Item;
        }
    }
}
