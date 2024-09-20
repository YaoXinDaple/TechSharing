using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

var normalDic = new Dictionary<int, int>();
normalDic.Add(1, 1);
normalDic.Add(2, 2);

var immutableDic = normalDic.ToImmutableDictionary();
immutableDic = immutableDic.Add(3, 3);

var readonlyDic = new ReadOnlyDictionary<int, int>(normalDic);
normalDic[1] = 3;

var frozenDic = normalDic.ToFrozenDictionary();
//frozenDic.Add(4, 4);


Console.WriteLine($"normalDic:{JsonConvert.SerializeObject(normalDic)}");
Console.WriteLine($"immutableDic:{JsonConvert.SerializeObject(immutableDic)}");
Console.WriteLine($"readonlyDic:{JsonConvert.SerializeObject(readonlyDic)}");
Console.WriteLine($"frozenDic:{JsonConvert.SerializeObject(frozenDic)}");
Console.ReadKey();


/*
 * ImmutableDictionary 包含Add、Remove、SetItem等方法，但是这些方法都会返回一个新的ImmutableDictionary实例，而不会修改原始实例。
 * ReadOnlyDictionary 也是只读的，但是它是对原始Dictionary的一个包装，所以原始Dictionary的修改会影响到ReadOnlyDictionary。
 * FrozenDictionary 专门用于优化读取性能，只提供读取相关的方法。
 */


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
