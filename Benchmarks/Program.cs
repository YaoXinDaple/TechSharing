
using Benchmarks.FrozenDictionaryBenchmarks;
using Benchmarks.ListLinqMethidBenchmarks;
using Benchmarks.StringComparisonBenchmarks;
using Benchmarks.StringConstructorBenchmarsk;
using Benchmarks.StringContainsBenchmarsk;
using Benchmarks.StringSplitBenchmarsk;
using System.Reflection;

List<Type> BenchmarkMethods = new List<Type>
{
    typeof(CopyWithArrayBenchmarks),
    typeof(DictionaryBenchmarks),
    typeof(ListBenchmarks),
    typeof(StringComparerBenchmarks),
    typeof(StringConstructorBenchmarks),
    typeof(StringContainsBenchmarks),
    typeof(StringBuilderVsStringConcatBenchmarks)
};


while (true)
{
    for (int i = 0; i < BenchmarkMethods.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {BenchmarkMethods[i].Name}");
    }
    Console.Write($"Please select the benchmark method you want to run(0-{BenchmarkMethods.Count - 1}):");
    var inputStr = Console.ReadLine();
    if (int.TryParse(inputStr, out int number))
    {
        if (number > 0 && number < BenchmarkMethods.Count)
        {
            Type type = BenchmarkMethods[number - 1];
            MethodInfo method = type.GetMethod("Run");
            method.Invoke(null, null);
        }

    }
    Console.WriteLine("本次基准测试运行完成，按任意键继续选择并运行基准测试");
    Console.ReadKey();
}

