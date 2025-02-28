using BenchmarkDotNet.Attributes;
using System.Threading.Channels;
using TaxItemCategorySync.EntityFrameworkCore.LemonInvoiceSuiteDbcontext;

namespace TaxItemCategorySync.SyncCustomer
{
    [MemoryDiagnoser]
    public class CustomerSyncClassedOrStructed
    {
        private InSystemCustomerSyncServiceClassed _inSystemCustomerSyncServiceClassed;
        private InSystemCustomerSyncServiceStructed _inSystemCustomerSyncServiceStructed;

        [Params("6689E6CC-A4A8-7B02-F8CF-3A1688DD3535")]
        public string CompanyIdStr { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var dbContext = new LemonInvoiceSuiteContext();
            _inSystemCustomerSyncServiceClassed = new InSystemCustomerSyncServiceClassed(dbContext);
            _inSystemCustomerSyncServiceStructed = new InSystemCustomerSyncServiceStructed(dbContext);
        }

        [Benchmark]
        public async Task Classed()
        {
            await _inSystemCustomerSyncServiceClassed.SyncCustomerFromInvoiceHistoryAsync(Guid.Parse(CompanyIdStr), CancellationToken.None);
        }

        [Benchmark]
        public async Task Structed()
        {
            await _inSystemCustomerSyncServiceStructed.SyncCustomerFromInvoiceHistoryAsync(Guid.Parse(CompanyIdStr), CancellationToken.None);
        }
    }
}
