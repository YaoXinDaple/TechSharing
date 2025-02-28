using BenchmarkDotNet.Attributes;
using System.Text.RegularExpressions;

namespace MemoryOptimizeExample
{
    /*
    | Method      | Mean        | Error     | StdDev      | Gen0      | Gen1      | Gen2     | Allocated  |
    |------------ |------------:|----------:|------------:|----------:|----------:|---------:|-----------:|
    | StrictSplit |    394.8 us |   1.06 us |     0.94 us |         - |         - |        - |       32 B |
    | RegexMatch  | 49,847.1 us | 988.70 us | 1,928.38 us | 2500.0000 | 2400.0000 | 800.0000 | 30606274 B |
     */
    [MemoryDiagnoser]
    public class StrictSplitOrRegexMatch
    {
        private string text = File.ReadAllText(@"《水浒传》.txt");

        private Dictionary<string, int> frequency = [];

        [Benchmark]
        public int StrictSplit()
        {
            foreach (var m in text.Split(' '))
            {
                var word = m;
                if (frequency.ContainsKey(word))
                {
                    frequency[word]++;
                }
                else
                {
                    frequency[word] = 1;
                }
            }
            return frequency.Count;
        }

        [Benchmark]
        public int RegexMatch()
        {
            foreach (Match m in Helpers.Words().Matches(text))
            {
                string word = m.Value;
                if (frequency.ContainsKey(word))
                {
                    frequency[word]++;
                }
                else
                {
                    frequency[word] = 1;
                }
            }
            return frequency.Count;
        }

    }

    public static partial class Helpers
    {
        [GeneratedRegex(@"\s+")]
        public static partial Regex WhiteSpace();

        [GeneratedRegex(@"\w+")]
        public static partial Regex Words();

        public static void Use<T>(T value)
        {

        }
    }
}
