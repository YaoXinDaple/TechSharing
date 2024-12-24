using System.Buffers;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


//从当前项目根目录读取文件
string text = File.ReadAllText(@"《水浒传》.txt");

Dictionary<string, int> frequency = [];
var lookup = frequency.GetAlternateLookup<ReadOnlySpan<char>>();


string[] names = ["宋江", "高俅", "林冲", "武松", "时迁", "方腊"];
SearchValues<string> searchValues = SearchValues.Create(names,StringComparison.OrdinalIgnoreCase);

Stopwatch sw = new();
while (true)
{
    long mem = GC.GetTotalAllocatedBytes();
    sw.Restart();
    for (int trial = 0; trial < 10; trial++)
    {
        #region 循环方式
        // 统计所有字符串的总出现次数
        int totalCount = 0;

        // 遍历字符串数组，累加每个字符串的出现次数
        foreach (string str in names)
        {
            int count = 0;
            int index = 0;

            // 查找当前字符串在文件内容中的出现次数
            while ((index = text.IndexOf(str, index, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                count++;
                index += str.Length; // 移动到下一个搜索位置
            }

            totalCount += count; // 累加到总计数
        }
        #endregion

        #region SearchValues

        //int count = 0;
        //var remaining = text.AsSpan();
        //while (!remaining.IsEmpty)
        //{
        //    int pos = remaining.IndexOfAny(searchValues);
        //    if (pos==-1)
        //    {
        //        break;
        //    }
        //    remaining = remaining.Slice(pos + 1);
        //    count++;
        //}

        ////Console.WriteLine($"出现次数：{count}");

        #endregion
    }
    sw.Stop();
    Helpers.Use(frequency);
    mem = GC.GetTotalAllocatedBytes() - mem;
    Console.WriteLine($"消耗时间：{sw.Elapsed.TotalMilliseconds}ms,使用内存:{mem / 1024.0 / 1024.0:N2}mb");
}

static partial class Helpers
{
    [GeneratedRegex(@"\s+")]
    public static partial Regex WhiteSpace();

    [GeneratedRegex(@"宋江")]
    public static partial Regex Words();

    public static void Use<T>(T value)
    {

    }
}



/*
 //源生成器
 [GeneratedRegex(@"\s+")]


Regex.EnumerateMatches()


GetAlternateLookup<T>(); T 可以是 ref struct 类型


Split  EnumerateSplits


CollectionsMarshal.GetValueRefOrAddDefault(lookup, word, out _)++;



SearchValues 和 IndexOfAny()

 */

