using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Frozen;

var summary = BenchmarkRunner.Run<DictionaryBenchmarks>();

[MemoryDiagnoser]
public class DictionaryBenchmarks
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


/*
 适用于
产品	版本
.NET	6, 7, 8, 9
.NET Framework	4.6.2, 4.7, 4.7.1, 4.7.2, 4.8, 4.8.1
.NET Standard	2.0
 
.NET Framework项目也可以使用FrozenDictionary类，但是需要安装System.Collections.Immutable NuGet包。
 */
