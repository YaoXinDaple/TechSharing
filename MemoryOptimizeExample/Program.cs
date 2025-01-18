// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using MemoryOptimizeExample;

Console.WriteLine("Hello, World!");


BenchmarkRunner.Run<UsingStructInsteadOfClass>();

Console.ReadLine();