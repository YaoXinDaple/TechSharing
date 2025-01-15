using BenchmarkDotNet.Attributes;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace TaxItemCategorySync
{
    [MemoryDiagnoser]
    public class IsValidEmailBenchmark
    {
        [Params("dapleyx@gmail.com;502415032@qq.com.")]
        public string Str { get; set; }

        [Benchmark]
        public bool IsValidEmailGroup()
        {
            if (string.IsNullOrWhiteSpace(Str))
            {
                return false;
            }

            return IsValidEmailGroup(Str.AsSpan());

        }

        private bool IsEmail([NotNullWhen(true)] ReadOnlySpan<char> str)
        {
            if (str.IsWhiteSpace())
            {
                return false;
            }
            if (!Regex.IsMatch(str, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$"))
            {
                return false;
            }
            return true;
        }


        private bool IsValidEmailGroup([NotNullWhen(true)] ReadOnlySpan<char> str)
        {
            if (str.IsWhiteSpace())
            {
                return false;
            }

            Span<Range> ranges = stackalloc Range[4]; // 假设最多分割成 10 段
            ReadOnlySpan<char> separators = ['；', ';'];

            int count = str.SplitAny(ranges, separators);
            if (count > 3)
            {
                throw new ArgumentException("最多支持3个邮箱");
            }
            for (int i = 0; i < count; i++)
            {
                if (!IsEmail(str[ranges[i]]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsEmailPlainStr([NotNullWhen(true)] string? str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            if (!Regex.IsMatch(str, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$"))
            {
                return false;
            }
            return true;
        }

        [Benchmark]
        public bool IsValidEmailGroupPlainStr()
        {
            if (string.IsNullOrWhiteSpace(Str))
            {
                return false;
            }

            char[] splitDelimeter = ['；', ';'];
            var splitEmails = Str.Split(splitDelimeter);
            foreach (var splitEmail in splitEmails)
            {
                if (!IsEmail(splitEmail))
                {
                    return false;
                }
            }
            return true;

        }

    }
}
