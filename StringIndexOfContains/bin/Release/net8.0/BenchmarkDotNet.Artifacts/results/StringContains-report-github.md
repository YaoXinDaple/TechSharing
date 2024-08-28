```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.3880/23H2/2023Update/SunValley3)
Intel Core i7-8700 CPU 3.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100-preview.3.24204.13
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2


```
| Method   | Mean      | Error     | StdDev    |
|--------- |----------:|----------:|----------:|
| IndexOf  |  1.743 ns | 0.0296 ns | 0.0247 ns |
| Contains |  1.346 ns | 0.0414 ns | 0.0387 ns |
| Any      | 28.434 ns | 0.5913 ns | 0.6809 ns |
