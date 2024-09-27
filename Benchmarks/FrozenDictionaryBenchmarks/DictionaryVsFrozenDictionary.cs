using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Frozen;
using System.ComponentModel;

namespace Benchmarks.FrozenDictionaryBenchmarks
{
    [MemoryDiagnoser]
    public class DictionaryVsFrozenDictionary
    {
        private Dictionary<int, int> _dictionary;
        private FrozenDictionary<int, int> _frozenDictionary;
        private const int Count = 10000;

        [GlobalSetup]
        public void Setup()
        {
            var data = new Dictionary<int, int>();
            for (int i = 0; i < Count; i++)
            {
                data.Add(i, i);
            }

            _dictionary = new Dictionary<int, int>(data);
            _frozenDictionary = _dictionary.ToFrozenDictionary();
        }

        [Benchmark]
        public void TestDictionary()
        {
            for (int i = 0; i < Count; i++)
            {
                var value = _dictionary[i];
            }
        }

        [Benchmark]
        public void TestFrozenDictionary()
        {
            for (int i = 0; i < Count; i++)
            {
                var value = _frozenDictionary[i];
            }
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