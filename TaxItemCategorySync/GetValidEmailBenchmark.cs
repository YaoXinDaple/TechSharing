using BenchmarkDotNet.Attributes;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace TaxItemCategorySync
{
    [MemoryDiagnoser]
    public class GetValidEmailBenchmark
    {

        [Params("dapleyx@gmail.com;502415032@qq.com;1551613@qq.com")]
        public string Email { get; set; }

        [Benchmark]
        public List<string> GetValidEmails()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                return [];
            }

            List<string> validEmails = [];
            char[] delimiters = [';', '；'];
            var splitStr = Email.Split(delimiters);
            foreach (var email in splitStr)
            {
                if (IsEmail(email))
                {
                    validEmails.Add(email);
                }
            }
            return validEmails;
        }

        [Benchmark]
        public List<string> GetValidEmailsWithSpan()
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

        private bool IsEmail([NotNullWhen(true)] ReadOnlySpan<char> str)
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
    }
}
