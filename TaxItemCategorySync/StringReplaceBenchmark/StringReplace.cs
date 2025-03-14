using BenchmarkDotNet.Attributes;

namespace TaxItemCategorySync.StringReplaceBenchmark
{

    /* 0.015在字符串中只出现一次。下面三组测试分别是在开头、中间、末尾。
     * [Params("0.015,0.03,0.01,0.06,0.09,0.15", "0.03,0.01,0.06,0.015,0.09,0.15", "0.03,0.01,0.06,0.09,0.15,0.015")]
     * 
    | Method        | InputString          | Mean     | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
    |-------------- |--------------------- |---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
    | Replace       | 0.015(...),0.15 [30] | 24.98 ns | 0.119 ns | 0.111 ns |  1.72 |    0.01 | 0.0043 |      72 B |        1.00 |
    | SplitThenJoin | 0.015(...),0.15 [30] | 76.17 ns | 0.532 ns | 0.472 ns |  5.24 |    0.05 | 0.0134 |     224 B |        3.11 |
    | UseSpan       | 0.015(...),0.15 [30] | 14.52 ns | 0.117 ns | 0.110 ns |  1.00 |    0.01 | 0.0043 |      72 B |        1.00 |
    |               |                      |          |          |          |       |         |        |           |             |
    | Replace       | 0.03,(...),0.15 [30] | 31.34 ns | 0.410 ns | 0.363 ns |  2.19 |    0.03 | 0.0043 |      72 B |        1.00 |
    | SplitThenJoin | 0.03,(...),0.15 [30] | 79.03 ns | 0.521 ns | 0.487 ns |  5.53 |    0.05 | 0.0153 |     256 B |        3.56 |
    | UseSpan       | 0.03,(...),0.15 [30] | 14.28 ns | 0.090 ns | 0.084 ns |  1.00 |    0.01 | 0.0043 |      72 B |        1.00 |
    |               |                      |          |          |          |       |         |        |           |             |
    | Replace       | 0.03,(...)0.015 [30] | 26.21 ns | 0.106 ns | 0.099 ns |  1.82 |    0.01 | 0.0043 |      72 B |        1.00 |
    | SplitThenJoin | 0.03,(...)0.015 [30] | 78.91 ns | 0.334 ns | 0.296 ns |  5.47 |    0.03 | 0.0134 |     224 B |        3.11 |
    | UseSpan       | 0.03,(...)0.015 [30] | 14.43 ns | 0.058 ns | 0.055 ns |  1.00 |    0.01 | 0.0043 |      72 B |        1.00 | 
    */


    /* 0.015在字符串中出现两次。下面三组测试分别是开始和末尾、开始和中间、中间和末尾。
     * [Params("0.015,0.03,0.01,0.06,0.09,0.15,0.015", "0.015,0.03,0.01,0.015,0.06,0.09,0.15", "0.03,0.01,0.015,0.06,0.09,0.15,0.015")]
     * 
    | Method        | InputString          | Mean     | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
    |-------------- |--------------------- |---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
    | Replace       | 0.015(...),0.15 [36] | 33.00 ns | 0.091 ns | 0.086 ns |  2.25 |    0.02 | 0.0043 |      72 B |        0.82 |
    | SplitThenJoin | 0.015(...),0.15 [36] | 87.17 ns | 0.443 ns | 0.414 ns |  5.94 |    0.05 | 0.0153 |     256 B |        2.91 |
    | UseSpan       | 0.015(...),0.15 [36] | 14.66 ns | 0.110 ns | 0.103 ns |  1.00 |    0.01 | 0.0052 |      88 B |        1.00 |
    |               |                      |          |          |          |       |         |        |           |             |
    | Replace       | 0.015(...)0.015 [36] | 38.76 ns | 0.181 ns | 0.169 ns |  2.65 |    0.02 | 0.0095 |     160 B |        1.82 |
    | SplitThenJoin | 0.015(...)0.015 [36] | 88.22 ns | 0.442 ns | 0.413 ns |  6.04 |    0.04 | 0.0138 |     232 B |        2.64 |
    | UseSpan       | 0.015(...)0.015 [36] | 14.61 ns | 0.076 ns | 0.064 ns |  1.00 |    0.01 | 0.0052 |      88 B |        1.00 |
    |               |                      |          |          |          |       |         |        |           |             |
    | Replace       | 0.03,(...)0.015 [36] | 40.25 ns | 0.135 ns | 0.120 ns |  2.76 |    0.01 | 0.0095 |     160 B |        1.82 |
    | SplitThenJoin | 0.03,(...)0.015 [36] | 92.05 ns | 1.049 ns | 0.981 ns |  6.31 |    0.07 | 0.0153 |     256 B |        2.91 |
    | UseSpan       | 0.03,(...)0.015 [36] | 14.59 ns | 0.057 ns | 0.053 ns |  1.00 |    0.00 | 0.0052 |      88 B |        1.00 |
     */
    [SimpleJob]
    [MemoryDiagnoser]
    public class StringReplace
    {
        private static string match1 = "0.015,";
        private static string match2 = ",0.015";

        [Params("0.015,0.03,0.01,0.06,0.09,0.15", "0.03,0.01,0.06,0.015,0.09,0.15", "0.03,0.01,0.06,0.09,0.15,0.015")]
        public string InputString { get; set; }

        [Benchmark]
        public string Replace()
        {
            return InputString.Replace(match1, "").Replace(match2, "");
        }

        [Benchmark]
        public string SplitThenJoin()
        {
            var parts = InputString.Split(new string[] { match1, match2 }, StringSplitOptions.None);
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == match1 || parts[i] == match2)
                {
                    parts[i] = "";
                }
            }
            return string.Join("", parts);
        }

        [Benchmark(Baseline = true)]
        public string UseSpan()
        {
            var span = InputString.AsSpan();

            int startIndex = span.IndexOfAny(match1);
            if (startIndex == 0)
            {
                var newString = span.Slice(match1.Length).ToString();
                return newString;
            }
            else if (startIndex > 0 && span.Length == startIndex + match1.Length)
            {
                if (span.Length == startIndex + match1.Length)
                {
                    return span.Slice(0, startIndex).ToString();
                }
                else
                {
                    var leftPart = span.Slice(0, startIndex);
                    var rightPart = span.Slice(startIndex + match1.Length);
                    var newSpan = Span<char>.Empty;
                    leftPart.CopyTo(newSpan);
                    rightPart.CopyTo(newSpan);
                    return newSpan.ToString();
                }
            }
            else
            {
                startIndex = span.IndexOf(match2);
                if (startIndex == 0)
                {
                    var newString = span.Slice(match2.Length).ToString();
                    return newString;
                }
                else if (startIndex > 0 && span.Length == startIndex + match2.Length)
                {
                    if (span.Length == startIndex + match2.Length)
                    {
                        return span.Slice(0, startIndex).ToString();
                    }
                    else
                    {
                        var leftPart = span.Slice(0, startIndex);
                        var rightPart = span.Slice(startIndex + match2.Length);
                        var newSpan = Span<char>.Empty;
                        leftPart.CopyTo(newSpan);
                        rightPart.CopyTo(newSpan);
                        return newSpan.ToString();
                    }
                }
            }
            return InputString;
        }
    }
}
