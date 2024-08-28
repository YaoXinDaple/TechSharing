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