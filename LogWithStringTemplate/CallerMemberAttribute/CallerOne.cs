namespace LogWithStringTemplate.CallerMemberAttribute
{
    public static class CallerOne
    {
        public static string GetCallerInfo()
        {
            return Service.SomeMethod("1");
        }
    }
}
