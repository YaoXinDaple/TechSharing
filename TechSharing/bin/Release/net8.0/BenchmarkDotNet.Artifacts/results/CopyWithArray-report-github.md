```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.3880/23H2/2023Update/SunValley3)
Intel Core i7-8700 CPU 3.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100-preview.3.24204.13
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2


```
| Method                     | Mean     | Error     | StdDev    | Gen0     | Gen1     | Gen2     | Allocated |
|--------------------------- |---------:|----------:|----------:|---------:|---------:|---------:|----------:|
| ProcessWithArray           | 3.992 ms | 0.0670 ms | 0.0626 ms | 617.1875 | 367.1875 | 242.1875 |   4.19 MB |
| ProcessWithList            | 5.735 ms | 0.1138 ms | 0.1217 ms | 828.1250 | 828.1250 | 500.0000 |   5.43 MB |
| ProcessWithListSetCapacity | 4.518 ms | 0.0857 ms | 0.0759 ms | 617.1875 | 367.1875 | 242.1875 |   4.19 MB |
