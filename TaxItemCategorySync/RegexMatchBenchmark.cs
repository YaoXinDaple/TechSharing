using BenchmarkDotNet.Attributes;
using System.Text.RegularExpressions;

namespace TaxItemCategorySync
{
    [MemoryDiagnoser]
    public class RegexMatchBenchmark
    {
        //结论：对单个字符串进行正则匹配，没有额外的内存分配

        public string PhoneNumber { get; set; } = "13800138000";

        [Benchmark]
        public bool IsPhoneNumber()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                return false;
            }

            // 中国手机号码
            if (!Regex.IsMatch(PhoneNumber, @"^1[3-9]\d{9}$"))
            {
                return false;
            }
            return true;
        }
    }
}
