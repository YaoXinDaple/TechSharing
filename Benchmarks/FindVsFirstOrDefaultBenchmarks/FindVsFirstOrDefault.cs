using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.ComponentModel;

namespace Benchmarks.FindVsFirstOrDefaultBenchmarks
{
    [Description("比较Find/FirstOrDefault方法在net8 和net9 之间的性能比较")]
    public static class FindVsFirstOrDefault
    {
        public static void Run()
        {
            BenchmarkRunner.Run<FindVsFirstOrDefaultBenchmark>();
        }
    }

    public class IntWarpper
    {
        public int Number { get; set; }
    }

    [MemoryDiagnoser(false)]
    public class FindVsFirstOrDefaultBenchmark
    {
        private static List<int> _list;
        private static List<IntWarpper> _listWrapper;

        [GlobalSetup]
        public void SetpUp()
        {
            _list = [.. Enumerable.Range(0, 10000)];
            for (int i = 0; i < 10000; i++)
            {
                _listWrapper.Add(new IntWarpper { Number = i });
            }
        }


        [Benchmark]
        public int Find()
        {
            return _list.Find(x => x == 5000);
        }

        [Benchmark]
        public int FirstOrDefault()
        {
            return _list.FirstOrDefault(x => x == 5000);
        }
    }
}
