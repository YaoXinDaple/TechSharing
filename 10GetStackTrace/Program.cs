using GetStackTrace.CallerMemberAttribute;
using Serilog;

//记录堆栈信息

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

CallerOne.GetCallerInfo();
CallerTwo.GetCallerInfo();