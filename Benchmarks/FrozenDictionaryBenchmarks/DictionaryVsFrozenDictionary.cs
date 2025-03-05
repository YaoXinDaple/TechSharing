using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Frozen;
using System.ComponentModel;
using System.Text;

namespace Benchmarks.FrozenDictionaryBenchmarks
{

    /*
     | Method                       | Mean      | Error     | StdDev    | Ratio | Allocated | Alloc Ratio |
     |----------------------------- |----------:|----------:|----------:|------:|----------:|------------:|
     | GetValueFromDictionary       | 17.350 ns | 0.0870 ns | 0.0814 ns |  1.00 |         - |          NA |
     | GetValueFromFrozenDictionary |  9.881 ns | 0.0957 ns | 0.0895 ns |  0.57 |         - |          NA |
     */
    [MemoryDiagnoser]
    public class DictionaryVsFrozenDictionary
    {
        private Dictionary<string, int> _dictionary;
        private FrozenDictionary<string, int> _frozenDictionary;

        private List<string> _randomKeys = [];
        private const int Count = 1000;

        private static readonly char[] AlterKeys = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        [GlobalSetup]
        public void Setup()
        {
            _dictionary = new Dictionary<string, int>();
            while (_randomKeys.Count < Count)
            {
                StringBuilder sb = new StringBuilder();
                int keyLength = Random.Shared.Next(3, 8);
                for (int j = 0; j < keyLength; j++)
                {
                    int randomIndex = Random.Shared.Next(0, AlterKeys.Length);
                    char partialKey = AlterKeys[randomIndex];
                    sb.Append(partialKey);
                }
                _randomKeys.Add(sb.ToString());
            }
            _randomKeys = [.. _randomKeys.Distinct()];

            for (int i = 0; i < _randomKeys.Count; i++)
            {
                _dictionary.TryAdd(_randomKeys[i], i);
            }

            _frozenDictionary = _dictionary.ToFrozenDictionary();
        }

        [Benchmark(Baseline = true)]
        public void GetValueFromDictionary()
        {
            int randomIndex = Random.Shared.Next(0, _randomKeys.Count);
            var value = _dictionary[_randomKeys[randomIndex]];
        }

        [Benchmark]
        public void GetValueFromFrozenDictionary()
        {
            int randomIndex = Random.Shared.Next(0, _randomKeys.Count);
            var value = _frozenDictionary[_randomKeys[randomIndex]];
        }
    }

    [Description("字典（Dictionary）和冻结字典（FrozenDictionary）的读取效率")]
    public static class DictionaryBenchmarks
    {
        public static void Run()
        {
            BenchmarkRunner.Run<DictionaryVsFrozenDictionary>();
        }
    }
}

/*
 * ImmutableDictionary 包含Add、Remove、SetItem等方法，但是这些方法都会返回一个新的ImmutableDictionary实例，而不会修改原始实例。
 * ReadOnlyDictionary 也是只读的，但是它是对原始Dictionary的一个包装，所以原始Dictionary的修改会影响到ReadOnlyDictionary。
 * FrozenDictionary 专门用于优化读取性能，只提供读取相关的方法。
 */

/*
 适用于
产品	版本
.NET	6, 7, 8, 9
.NET Framework	4.6.2, 4.7, 4.7.1, 4.7.2, 4.8, 4.8.1
.NET Standard	2.0
 
.NET Framework项目也可以使用FrozenDictionary类，但是需要安装System.Collections.Immutable NuGet包。
 */