```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.3880/23H2/2023Update/SunValley3)
Intel Core i7-8700 CPU 3.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100-preview.3.24204.13
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2


```
| Method                    | Mean     | Error    | StdDev   | Gen0   | Allocated |
|-------------------------- |---------:|---------:|---------:|-------:|----------:|
| CompareWithToLower        | 82.75 ns | 0.896 ns | 0.838 ns | 0.0484 |     304 B |
| CompareWithEqualsOverload | 13.88 ns | 0.057 ns | 0.047 ns |      - |         - |
