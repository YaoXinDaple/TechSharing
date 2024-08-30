```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4037/23H2/2023Update/SunValley3)
Intel Core i7-8700 CPU 3.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.304
  [Host]     : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2 [AttachedDebugger]
  Job-VPYKCV : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2

IterationCount=1  LaunchCount=1  WarmupCount=1  

```
| Method                    | Mean     | Error | Gen0   | Allocated |
|-------------------------- |---------:|------:|-------:|----------:|
| CompareWithToLower        | 79.25 ns |    NA | 0.0484 |     304 B |
| CompareWithEqualsOverload | 15.91 ns |    NA |      - |         - |
