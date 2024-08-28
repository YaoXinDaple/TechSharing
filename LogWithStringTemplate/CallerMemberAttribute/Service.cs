using Serilog;
using System.Runtime.CompilerServices;

namespace LogWithStringTemplate.CallerMemberAttribute
{
    public static class Service
    {
        public static string SomeMethod(
            string input,
            [CallerMemberName] string callerMember = default!,
            [CallerLineNumber] int callerLineNumber = default,
            [CallerFilePath] string filePath = default!,
            [CallerArgumentExpression("input")] string argument = default!)
        {
            Log.Logger.Information($"Message from {callerMember} at line {callerLineNumber} in file:{filePath}.argument:input = {input}");
            return $"Message from {callerMember} at line {callerLineNumber}";
        }
    }
}