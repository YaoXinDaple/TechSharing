using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Benchmarks.FrozenDictionaryBenchmarks
{
    /*
     | Method                 | Mean      | Error     | StdDev    | Ratio | RatioSD |
     |----------------------- |----------:|----------:|----------:|------:|--------:|
     | CreateDictionary       |  6.770 us | 0.0342 us | 0.0320 us |  1.00 |    0.01 |
     | CreateFrozenDictionary | 85.831 us | 0.8736 us | 0.8172 us | 12.68 |    0.13 |
     */
    public class FrozenDictionaryBuild
    {
        private const int Count = 1000;
        private List<string> RandomKeys = new List<string>();

        private Dictionary<string, int> _normalDic;
        private FrozenDictionary<string, int> _frozenDic;
        private static readonly char[] AlterKeys = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        [GlobalSetup]
        public void Setup()
        {
            // 初始化 _normalDic
            _normalDic = new Dictionary<string, int>();
            while (RandomKeys.Count < Count)
            {
                StringBuilder sb = new StringBuilder();
                int keyLength = Random.Shared.Next(3, 8);
                for (int j = 0; j < keyLength; j++)
                {
                    int randomIndex = Random.Shared.Next(0, AlterKeys.Length);
                    char partialKey = AlterKeys[randomIndex];
                    sb.Append(partialKey);
                }
                RandomKeys.Add(sb.ToString());
            }
            RandomKeys = [.. RandomKeys.Distinct()];
        }

        [Benchmark(Baseline = true)]
        public void CreateDictionary()
        {
            for (int i = 0; i < RandomKeys.Count; i++)
            {
                _normalDic.TryAdd(RandomKeys[i], i);
            }
        }

        [Benchmark]
        public void CreateFrozenDictionary()
        {
            var data = new Dictionary<string, int>();
            for (int i = 0; i < RandomKeys.Count; i++)
            {
                data.TryAdd(RandomKeys[i], i);
            }
            var frozenDic = data.ToFrozenDictionary();
        }
    }

    [Description("字典（Dictionary）和冻结字典（FrozenDictionary）的构建效率")]
    public static class FrozenDictionaryBuildBenchmarks
    {
        public static void Run()
        {
            BenchmarkRunner.Run<FrozenDictionaryBuild>();
        }
    }
}
