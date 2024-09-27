using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.ComponentModel;

namespace Benchmarks.StringContainsBenchmarsk
{
    public class StringContains
    {
        const string testString = """
        THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
            THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
                THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
                    THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
                        THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
        """;

        const char testChar = 'o';

        [Benchmark]
        public bool IndexOf() => testString.IndexOf(testChar) >= 0;

        [Benchmark]
        public bool Contains() => testString.Contains(testChar);

        [Benchmark]
        public bool Any() => testString.Any(c => c.Equals(testChar));
    }

    [Description("字符串包含")]
    public static class StringContainsBenchmarks
    {
        public static void Run()
        {
            BenchmarkRunner.Run<StringContains>();
        }
    }

    /*
     * 结论：
     * 应尽可能不要使用Linq的Any方法，大部分情况下性能都是最差的。
     */
}
