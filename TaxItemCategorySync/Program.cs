// See https://aka.ms/new-console-template for more information
using TaxItemCategorySync;

Console.WriteLine("Hello, World!");



BenchmarkDotNet.Running.BenchmarkRunner.Run<SyncTaxItemCategoryBenchmark>();

Console.ReadLine();