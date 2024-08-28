using LogWithStringTemplate.CallerMemberAttribute;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();


CallerOne.GetCallerInfo();
CallerTwo.GetCallerInfo();


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

Console.WriteLine();
Console.WriteLine("Ready To Log");
Console.ReadKey();
