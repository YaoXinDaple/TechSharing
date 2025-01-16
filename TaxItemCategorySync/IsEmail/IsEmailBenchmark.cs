using BenchmarkDotNet.Attributes;
using System.Text.RegularExpressions;

namespace TaxItemCategorySync.IsEmail
{
    [MemoryDiagnoser]
    public class IsEmailBenchmark
    {
        [Params("dapleyx@gmail.com", "ggfsfsdfsdfsdfsf")]
        public string Email { get; set; }

        [Benchmark]
        public bool IsEmail()
        {
            var span = Email.AsSpan();
            if (span.IsWhiteSpace())
            {
                return false;
            }
            if (!Regex.IsMatch(span, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$"))
            {
                return false;
            }
            return true;
        }



        [Benchmark]
        public bool IsEmailContainsFirst()
        {
            var span = Email.AsSpan();
            if (!span.Contains('@'))
            {
                return false;
            }
            if (span.IsWhiteSpace())
            {
                return false;
            }
            if (!Regex.IsMatch(span, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$"))
            {
                return false;
            }
            return true;
        }
    }
}
