﻿using BenchmarkDotNet.Attributes;
using System.ComponentModel;
using System.Text;

namespace Benchmarks.StringSplitBenchmarks
{

    [MemoryDiagnoser]
    public class StringBuilderVsStringConcat
    {
        [Benchmark]
        public string StringConcat()
        {
            string result = string.Empty;
            for (int i = 0; i < 1000; i++)
            {
                result += i;
            }
            return result;
        }

        [Benchmark]
        public string StringBuilder()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                sb.Append(i);
            }
            return sb.ToString();
        }
    }

    [Description("字符串拼接和StringBuilder")]
    public static class StringBuilderVsStringConcatBenchmarks
    {
        public static void Run()
        {
            BenchmarkDotNet.Running.BenchmarkRunner.Run<StringBuilderVsStringConcat>();
        }
    }
}
