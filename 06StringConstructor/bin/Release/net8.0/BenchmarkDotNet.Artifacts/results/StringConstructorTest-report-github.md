```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4037/23H2/2023Update/SunValley3)
Intel Core i7-8700 CPU 3.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100-preview.3.24204.13
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2


```
| Method                 | Mean        | Error     | StdDev    | Median      | Gen0   | Gen1   | Allocated |
|----------------------- |------------:|----------:|----------:|------------:|-------:|-------:|----------:|
| ReverseByJoin          | 2,064.18 ns | 13.150 ns | 10.267 ns | 2,065.57 ns | 0.7362 |      - |    4640 B |
| ReverseWithConstructor |   212.20 ns | 20.416 ns | 58.906 ns |   184.85 ns | 0.3710 | 0.0007 |    2328 B |
| ReverseWithAsSpan      |    73.05 ns |  1.329 ns |  1.243 ns |    73.29 ns | 0.1211 |      - |     760 B |
