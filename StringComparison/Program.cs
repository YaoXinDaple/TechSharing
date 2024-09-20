using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<StringComparer>();

[MemoryDiagnoser]
public class StringComparer
{
    private  const string _target = "THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE";
    private  const string _source = "this is a long word will ALLOCATE MORE than one bit memory space";

    [Benchmark]
    public bool CompareWithToLower()=> _target.ToLower() == _source.ToLower();

    [Benchmark]
    public bool CompareWithEqualsOverload()=> _target.Equals(_source, StringComparison.OrdinalIgnoreCase);
}


/*
 * 结论：
 * 两个字符串的比较，永远不要ToLower之后比较，应该使用Equals方法或者根据实际情况确定是否要使用方法的重载
 */