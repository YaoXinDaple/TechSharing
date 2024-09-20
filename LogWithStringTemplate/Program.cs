using LogWithStringTemplate.CallerMemberAttribute;
using Serilog;
using Serilog.Core;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

//记录堆栈信息
//CallerOne.GetCallerInfo();
//CallerTwo.GetCallerInfo();


Console.WriteLine("Ready To Log");
Console.ReadKey();

for (int i = 0; i < 12345; i++)
{
    Log.Debug($"""
        This is message {i}.
        Serilog is a diagnostic logging library for .NET applications. 
        It is easy to set up, has a clean API, and runs on all recent .NET platforms. 
        While it's useful even in the simplest applications, 
        Serilog's support for structured logging shines when instrumenting complex, distributed, and asynchronous applications and systems.

        This is message {i}.
        Serilog is a diagnostic logging library for .NET applications. 
        It is easy to set up, has a clean API, and runs on all recent .NET platforms. 
        While it's useful even in the simplest applications, 
        Serilog's support for structured logging shines when instrumenting complex, distributed, and asynchronous applications and systems.

        This is message {i}.
        Serilog is a diagnostic logging library for .NET applications. 
        It is easy to set up, has a clean API, and runs on all recent .NET platforms. 
        While it's useful even in the simplest applications, 
        Serilog's support for structured logging shines when instrumenting complex, distributed, and asynchronous applications and systems.
        """, i);
}

#region 不使用字符串插值，记录日志的一些情况的写法
var strList = new List<string> { "a", "b", "c" };
var strList2 = new List<string> { "x", "y", "z" };
int num = 3;

var p = new
{
    Name = "Jim",
    Age = 13
};
Log.Information("The list is {StrList}", string.Join(",", strList));
Log.Information("The list is {StrList},num = {Num}", string.Join(",", strList), num);
Log.Information("The list is {StrList2},list2 is {StrList}", string.Join(",", strList), string.Join(",", strList2));
Log.Information("p = {p}", p); 
#endregion

Console.WriteLine();
Console.WriteLine("Ready To Log");
Console.ReadKey();
