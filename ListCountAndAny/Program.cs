using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


BenchmarkRunner.Run<ListBenchmark>();


public class ListBenchmark
{
    private List<int> emptyList = new List<int>();
    private List<int> nonEmptyList = new List<int> { 1 };


    [Benchmark]
    public bool EmptyListCountCheck()
    {
        return emptyList.Count == 0;
    }

    [Benchmark]
    public bool EmptyListAnyCheck()
    {
        return !emptyList.Any();
    }

    [Benchmark]
    public bool NonEmptyListCountCheck()
    {
        return nonEmptyList.Count == 0;
    }

    [Benchmark]
    public bool NonEmptyListAnyCheck()
    {
        return !nonEmptyList.Any();
    }


    //private List<int> largeList = new List<int>(Enumerable.Range(0, 10000));

    //private IEnumerable<int> largeEnumerable = new List<int>(Enumerable.Range(0, 10000));

    //[Benchmark]
    //public int GetLastWithLinq()
    //{
    //    return largeList.Last();
    //}

    //[Benchmark]
    //public int GetLastWithIndexer()
    //{
    //    return largeList[^1];
    //}
}



/*
 在C#中，对于检查一个集合是否为空，Count == 0 和 Any() 方法通常用于 List<T> 和 IEnumerable<T> 类型。这两种方法的效率对比取决于使用它们的具体集合类型。

对于 List<T> 类型：

Count 属性是一个 O(1) 操作，因为 List<T> 维护了一个内部计数器来跟踪元素的数量。因此，list.Count == 0 会立即返回结果，非常快速。

Any() 方法是一个扩展方法，它在 IEnumerable<T> 上工作。当应用于 List<T> 时，它通常会检查枚举器的第一个元素来决定是否有任何元素。对于 List<T> 来说，这也是一个快速的操作，但由于方法调用和枚举器的创建，它可能比直接访问 Count 属性要慢一点。

对于 IEnumerable<T> 类型：

如果你使用 Count() 方法（注意这是一个方法，不是属性），它将枚举整个集合来计算元素的数量，这是一个 O(n) 操作，其中 n 是集合中的元素数量。如果集合为空或者只有少数元素，这个操作还是很快的，但如果集合很大，这将是一个非常耗时的操作。

Any() 方法是专门设计来检查是否至少有一个元素的。它会尽快返回结果，通常在找到第一个元素时就停止枚举。这使得 Any() 成为检查 IEnumerable<T> 是否有元素的首选方法，因为它是一个 O(1) 操作，只要集合不为空。

因此，对于 List<T> 类型，Count == 0 和 Any() 都很高效，但 Count == 0 略胜一筹。对于 IEnumerable<T> 类型，Any() 要比 Count() 方法高效得多，特别是对于大型集合。在实践中，推荐使用 Any() 来检查任何 IEnumerable<T> 是否为空，因为它总是提供了最佳的性能，尤其是对于那些不知道具体大小或者可能非常大的集合。
 */