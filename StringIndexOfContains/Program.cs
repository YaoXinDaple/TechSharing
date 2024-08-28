
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<StringContains>();

public class StringContains
{
    const string testString = """
        THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
            THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
                THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
                    THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
                        THIS IS A long word wiLL ALLOCATE MORE than one bit MEMORY SPACE
        """;

    const char testChar = 'o';

    [Benchmark]
    public bool IndexOf() => testString.IndexOf(testChar) >= 0;

    [Benchmark]
    public bool Contains() => testString.Contains(testChar);

    [Benchmark]
    public bool Any() => testString.Any(c => c.Equals(testChar));

}


