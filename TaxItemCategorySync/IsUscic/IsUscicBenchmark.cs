using BenchmarkDotNet.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace TaxItemCategorySync.IsUscic
{
    [MemoryDiagnoser]
    public class IsUscicBenchmark
    {
        [Params("1234567890Z", "1234567890I", "Z234567890Z", "I234567890I", "12345Z7890Z", "12345I7890I")]
        public string USCIC { get; set; }

        private const string IllegalUscic = "IOSVZ";

        [Benchmark]
        public bool IsUscicChanged()
        {
            if (USCIC is null || USCIC.Length > 20)
            {
                return false;
            }

            //纳税人识别号只能输入数字或者「IOSVZ」以外的大写字母
            ReadOnlySpan<char> span = USCIC.AsSpan();
            ReadOnlySpan<char> illigalChars = IllegalUscic.AsSpan();

            foreach (char c in span)
            {
                if (!(char.IsDigit(c) || !illigalChars.Contains(c)))
                {
                    return false;
                }
            }

            return true;
        }


        [Benchmark]
        public bool IsUscicOrigin()
        {
            if (USCIC is null || USCIC.Length > 20)
            {
                return false;
            }

            //纳税人识别号只能输入数字或者「IOSVZ」以外的大写字母
            ReadOnlySpan<char> span = USCIC.AsSpan();

            foreach (char c in span)
            {
                if (!(char.IsDigit(c) || c >= 'A' && c <= 'H' || c >= 'J' && c <= 'N' ||
                      c >= 'P' && c <= 'R' || c >= 'T' && c <= 'U' || c >= 'W' && c <= 'Y'))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
