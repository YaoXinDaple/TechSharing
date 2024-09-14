namespace LogWithStringTemplate.CallerMemberAttribute
{
    internal class CallerTwo
    {
        public static string GetCallerInfo()
        {
            return Service.SomeMethodWithReflection("2");
        }
    }
}
