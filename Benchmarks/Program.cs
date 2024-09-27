
using Benchmarks.FrozenDictionaryBenchmarks;
using Benchmarks.ListLinqMethidBenchmarks;
using Benchmarks.StringComparisonBenchmarks;
using Benchmarks.StringConstructorBenchmarsk;
using Benchmarks.StringContainsBenchmarsk;
using Benchmarks.StringSplitBenchmarsk;
using System.ComponentModel;
using System.Reflection;

List<Type> BenchmarkMethods = new List<Type>
{
    typeof(FillDataToArrayOrListBenchmarks),
    typeof(DictionaryBenchmarks),
    typeof(ListBenchmarks),
    typeof(StringComparerBenchmarks),
    typeof(StringConstructorBenchmarks),
    typeof(StringContainsBenchmarks),
    typeof(StringBuilderVsStringConcatBenchmarks)
};


Console.ResetColor();
Console.ForegroundColor = ConsoleColor.White;
while (true)
{
    for (int i = 0; i < BenchmarkMethods.Count; i++)
    {
        Type type = BenchmarkMethods[i];
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{i + 1}. {type.Name}");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"    （{type.GetCustomAttribute<DescriptionAttribute>()?.Description}）");
    }
    Console.WriteLine();
    Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("请输入你想运行的基准测试的序号");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write($"(1-{BenchmarkMethods.Count - 1}):");
    var inputStr = Console.ReadLine();
    if (int.TryParse(inputStr, out int number))
    {
        if (number > 0 && number <= BenchmarkMethods.Count)
        {
            Type type = BenchmarkMethods[number - 1];
            MethodInfo method = type.GetMethod("Run");
            Console.WriteLine("开始运行基准测试：" + type.Name);
            method.Invoke(null, null);
        }

    }
}

