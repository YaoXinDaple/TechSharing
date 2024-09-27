using BenchmarkDotNet.Attributes;

namespace Benchmarks.StringConstructorBenchmarsk
{
    [MemoryDiagnoser]
    public class StringConstructorBenchmark
    {
        private const string originalStr = """
        THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
            THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
                THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
                    THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
                        THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
        """;

        [Benchmark]
        public string ReverseByJoin()
        {
            var array = originalStr.ToCharArray();
            //array = array.Reverse().ToArray();
            Array.Reverse(array);
            return string.Join("", originalStr.Reverse());
        }

        [Benchmark]
        public string ReverseWithConstructor()
        {
            var array = originalStr.ToCharArray();
            //array = array.Reverse().ToArray();
            Array.Reverse(array);
            return new string(array);
        }

        [Benchmark]
        public string ReverseWithAsSpan()
        {
            Span<char> span = stackalloc char[originalStr.Length];
            originalStr.AsSpan().CopyTo(span);
            span.Reverse();
            return new string(span);
        }
    }

    public static class StringConstructorBenchmarks
    {
        public static void Run()
        {
            BenchmarkDotNet.Running.BenchmarkRunner.Run<StringConstructorBenchmark>();
        }
    }
}
