
//使用LinQPad运行

/*
 

//string s = "Hello C# world!";

//var result = s.Split(' ');

//var result = s.Split(' ',2);//使用分隔符将原始字符串分割为两部分


=================================================================================================


//string s = "Hello ,    C# ,,   world!";

//var result = s.Split(',',  StringSplitOptions.RemoveEmptyEntries);//如果两个分隔符之间没有字符，结果中不会返回这部分

//var result = s.Split(',',StringSplitOptions.TrimEntries);//返回结果中的每个元素都会去掉前后的空格


//result.Dump();
*/


//字符串的构造函数
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

//string str = new string('=', 20);


//string s= "Hello world!";
//string.Join("", s.Reverse().ToArray());


//char[] chars = { 'h', 'e', 'l', 'l','o',',','w','o','r','l','d','!' };
//var reversedArr = chars.Reverse().ToArray();
//string str2 = new string(reversedArr);

BenchmarkRunner.Run<StringBuilderVsStringConcat>();

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