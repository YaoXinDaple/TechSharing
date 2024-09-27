using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.ComponentModel;

[MemoryDiagnoser]
public class FillDataToArrayOrList
{
    private IEnumerable<int> _list = Enumerable.Range(0, 100000);

    [Benchmark]
    public string[] ProcessWithArray()
    {
        var array = new string[_list.Count()]; // 单次调用 Count()
        int index = 0;
        foreach (var item in _list)
        {
            array[index++] = (item + item).ToString(); // 不需要 ToString()
        }
        return array;
    }

    [Benchmark]
    public List<string> ProcessWithList()
    {
        var list = new List<string>();

        foreach (int item in _list)
        {
            list.Add((item + item).ToString());
        }
        return list;
    }

    [Benchmark]
    public List<string> ProcessWithListSetCapacity()
    {
        var list = new List<string>(_list.Count());

        foreach (int item in _list)
        {
            list.Add((item + item).ToString());
        }
        return list;
    }

}

[Description("向数组或列表填充固定数量的数据")]
public static class FillDataToArrayOrListBenchmarks
{
    public static void Run()
    {
        BenchmarkRunner.Run<FillDataToArrayOrList>();
    }
}
