using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<CopyWithArray>();

[MemoryDiagnoser]
public class CopyWithArray
{
    private IEnumerable<int> _list = Enumerable.Range(0, 100000);

    public CopyWithArray()
    {
        _list = Enumerable.Range(0, 100000);
    }

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
