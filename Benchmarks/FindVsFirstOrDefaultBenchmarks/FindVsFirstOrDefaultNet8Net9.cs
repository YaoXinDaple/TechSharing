using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System.ComponentModel;

namespace Benchmarks.FindVsFirstOrDefaultBenchmarks
{
    [Description("比较Find/FirstOrDefault方法在net8 和net9 之间的性能比较")]
    public static class FindVsFirstOrDefaultNet8Net9
    {
        public static void Run()
        {
            BenchmarkRunner.Run<FindVsFirstOrDefaultBenchmark>();
        }
    }

    public class IntWrapper
    {
        public int Number { get; set; }
    }

    [MemoryDiagnoser(false)]
    [ShortRunJob(RuntimeMoniker.Net80)]
    [ShortRunJob(RuntimeMoniker.Net90)]
    public class FindVsFirstOrDefaultBenchmark
    {
        private static List<int> _list = [];
        private static List<IntWrapper> _listWrapper = [];

        [GlobalSetup]
        public void SetUp()
        {
            _list = [.. Enumerable.Range(0, 10000)];
            for (int i = 0; i < 10000; i++)
            {
                _listWrapper.Add(new IntWrapper { Number = i });
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

        [Benchmark]
        public int FindWrapper()
        {
            return _listWrapper.Find(x => x.Number == 5000).Number;
        }

        [Benchmark]
        public int FirstOrDefaultWrapper()
        {
            return _listWrapper.FirstOrDefault(x => x.Number == 5000).Number;
        }
    }
}
