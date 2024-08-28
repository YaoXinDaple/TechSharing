```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4037/23H2/2023Update/SunValley3)
Intel Core i7-8700 CPU 3.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.304
  [Host]     : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2


```
| Method               | Mean     | Error    | StdDev   | Allocated |
|--------------------- |---------:|---------:|---------:|----------:|
| TestDictionary       | 42.92 μs | 0.299 μs | 0.265 μs |         - |
| TestFrozenDictionary | 26.32 μs | 0.295 μs | 0.262 μs |         - |
