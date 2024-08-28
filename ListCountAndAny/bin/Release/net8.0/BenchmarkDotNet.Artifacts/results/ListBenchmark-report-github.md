```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4037/23H2/2023Update/SunValley3)
Intel Core i7-8700 CPU 3.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.304
  [Host]     : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2


```
| Method                 | Mean      | Error     | StdDev    | Median    |
|----------------------- |----------:|----------:|----------:|----------:|
| EmptyListCountCheck    | 0.0082 ns | 0.0162 ns | 0.0173 ns | 0.0000 ns |
| EmptyListAnyCheck      | 3.2029 ns | 0.0968 ns | 0.1977 ns | 3.1844 ns |
| NonEmptyListCountCheck | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
| NonEmptyListAnyCheck   | 3.2004 ns | 0.0959 ns | 0.1602 ns | 3.1918 ns |
