using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;
using TaxItemCategorySync.EntityFrameworkCore.LemonInvoiceSuiteDbcontext;

namespace TaxItemCategorySync.SyncTaxItemCategory
{
    [MemoryDiagnoser]
    public class SyncTaxItemCategoryBenchmark
    {
        public List<LemonTaxItemCategory> lemonTaxItemCategories = [];
        public void Setup()
        {
            var dbcontext = new LemonInvoiceSuiteContext();
            lemonTaxItemCategories = dbcontext.LemonTaxItemCategories.ToList();
            if (lemonTaxItemCategories.Count == 0)
            {
                throw new InvalidOperationException("没有获取到LemonTaxItemCategories数据");
            }
        }

        [Benchmark]
        public async Task SyncWithAsSpan()
        {
            LemonTaxItemCategory[] categories = new LemonTaxItemCategory[lemonTaxItemCategories.Count];
            for (int i = 0; i < CollectionsMarshal.AsSpan(lemonTaxItemCategories).Length; i++)
            {
                var item = lemonTaxItemCategories[i];
                var category = new LemonTaxItemCategory(item.Id, item.Name, item.TaxCode);
                category.ShortName = item.ShortName;
                category.Description = item.Description;
                category.ParentId = item.ParentId;
                category.TaxRateTypes = item.TaxRateTypes;
                category.ParentTaxCode = item.ParentTaxCode;
                category.AvailableForDifferentialTaxation = item.AvailableForDifferentialTaxation;
                category.RealTimeTaxRebateSign = item.RealTimeTaxRebateSign;
                category.IsEndLevel = item.IsEndLevel;
                category.KeyWord = item.KeyWord;

                categories[i] = category;
            }
            await Task.CompletedTask;
        }

        [Benchmark]
        public async Task SyncWithList()
        {
            LemonTaxItemCategory[] categories = new LemonTaxItemCategory[lemonTaxItemCategories.Count];
            for (int i = 0; i < lemonTaxItemCategories.Count; i++)
            {
                var item = lemonTaxItemCategories[i];
                var category = new LemonTaxItemCategory(item.Id, item.Name, item.TaxCode);
                category.ShortName = item.ShortName;
                category.Description = item.Description;
                category.ParentId = item.ParentId;
                category.TaxRateTypes = item.TaxRateTypes;
                category.ParentTaxCode = item.ParentTaxCode;
                category.AvailableForDifferentialTaxation = item.AvailableForDifferentialTaxation;
                category.RealTimeTaxRebateSign = item.RealTimeTaxRebateSign;
                category.IsEndLevel = item.IsEndLevel;
                category.KeyWord = item.KeyWord;

                categories[i] = category;
            }
            await Task.CompletedTask;
        }

    }
}
