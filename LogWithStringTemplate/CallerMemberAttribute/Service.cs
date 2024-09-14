using Serilog;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LogWithStringTemplate.CallerMemberAttribute
{
    public static class Service
    {
        public static string SomeMethodWithAttribute(
            string input,
            [CallerMemberName] string callerMember = default!,
            [CallerLineNumber] int callerLineNumber = default,
            [CallerFilePath] string filePath = default!,
            [CallerArgumentExpression("input")] string argument = default!)
        {
            Log.Logger.Information($"Message from {callerMember} at line {callerLineNumber} in file:{filePath}.argument:input = {input}");
            return $"Message from SomeMethod";
        }

        public static string SomeMethodWithReflection(string input)
        {
            #region 反射方式获取调用方信息

            // 获取当前堆栈信息
            StackTrace stackTrace = new StackTrace();
            // 获取调用方的方法信息
            StackFrame callingFrame = stackTrace.GetFrame(1)!;//1 表示上一层调用的方法
            MethodBase callingMethod = callingFrame.GetMethod()!;
            string callingMethodName = callingMethod.Name;
            string callingClassName = callingMethod.ReflectedType!.FullName!;

            #endregion

            Log.Logger.Information($"Message from {callingMethodName} ,className:{callingClassName}");
            return $"Message from SomeMethodWithReflection";
        }


    }
}