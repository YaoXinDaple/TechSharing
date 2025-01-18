// See https://aka.ms/new-console-template for more information
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using TaxItemCategorySync.GetValidEmail;

Console.WriteLine("Hello, World!");

BenchmarkDotNet.Running.BenchmarkRunner.Run<GetValidEmailBenchmark>();

Console.ReadLine();


List<string> GetValidEmailsWithSpan(string Email)
{
    if (string.IsNullOrWhiteSpace(Email))
    {
        return [];
    }

    var strSpan = Email.AsSpan();
    Span<Range> ranges = stackalloc Range[3];
    ReadOnlySpan<char> separators = [';', '；'];

    //ranges从常量上定义了最大的邮箱地址数量之后，这里只会读取常量中设置的数量
    int count = strSpan.SplitAny(ranges, separators);
    List<string> emailList = new List<string>(count);

    for (int i = 0; i < count; i++)
    {
        if (!IsEmail(strSpan[ranges[i]]))
        {
            emailList[i] = strSpan[ranges[i]].ToString();
        }
    }
    return emailList;
}
bool IsValidEmailGroup([NotNullWhen(true)] ReadOnlySpan<char> str)
{
    if (str.IsWhiteSpace())
    {
        return false;
    }

    Span<Range> ranges = stackalloc Range[3];
    ReadOnlySpan<char> separators = [';','；'];

    int count = str.SplitAny(ranges, separators);
    if (count > 3)
    {
        throw new ArgumentException($"邮箱地址应该不超过3个");
    }
    for (int i = 0; i < count; i++)
    {
        if (!IsEmail(str[ranges[i]]))
        {
            return false;
        }
    }

    return true;
}
bool IsEmail([NotNullWhen(true)]  ReadOnlySpan<char> str)
{
    if (str.IsWhiteSpace())
    {
        return false;
    }
    if (!Regex.IsMatch(str, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$"))
    {
        return false;
    }
    return true;
}

List<string> GetValidEmails([NotNullWhen(true)] string? str)
{
    if (string.IsNullOrWhiteSpace(str))
    {
        return [];
    }

    var strSpan = str.AsSpan();
    Span<Range> ranges = stackalloc Range[3];
    ReadOnlySpan<char> separators = [';', '；'];

    //ranges从常量上定义了最大的邮箱地址数量之后，这里只会读取常量中设置的数量
    int count = strSpan.SplitAny(ranges, separators);
    var emailList = new List<string>(count);

    for (int i = 0; i < count; i++)
    {
        if (!IsEmail(strSpan[ranges[i]]))
        {
            emailList.Add(strSpan[ranges[i]].ToString());
        }
    }
    return emailList;
}