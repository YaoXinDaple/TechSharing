using BenchmarkDotNet.Running;
using MemoryOptimizeExample;
using System.Reflection;

public class Program
{
    public static KeyValuePair<string, PropertyInfo[]> GlAccountAatRelatedProperties { get; set; }

    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        //GLAccount account = new GLAccount();
        //var propertyInfo = GetAccountAatRelatedProperties(account);
        ////从PropertyInfo中读取AcctProjectCode1到AcctProjectCode6属性
        //string[] aatValues = new string[propertyInfo.Length];
        //for (int i = 0; i < propertyInfo.Length; i++)
        //{
        //    aatValues[i] = propertyInfo[i].GetValue(account)==null?"": propertyInfo[i].GetValue(account).ToString()!;
        //}
        //Console.WriteLine(aatValues);

        BenchmarkRunner.Run<UsingCachedReflectionOrDelegate>();

        Console.ReadLine();
    }

    private static string[] GetAccountAatRelatedPropertiesWithDelegate(GLAccount account)
    {
        Type type = typeof(GLAccount);
        string[] aatPropertyNames = new string[] { "AcctProjTypeCode1", "AcctProjTypeCode2", "AcctProjTypeCode3", "AcctProjTypeCode4", "AcctProjTypeCode5", "AcctProjTypeCode6" };
        string[] aatValues = new string[aatPropertyNames.Length];
        for (int i = 0; i < aatPropertyNames.Length; i++)
        {
            PropertyInfo property = type.GetProperty(aatPropertyNames[i]);
            MemberGetDelegate memberGet = (MemberGetDelegate)System.Delegate.CreateDelegate(typeof(MemberGetDelegate), property.GetGetMethod());
            aatValues[i] = memberGet(account);
        }
        return aatValues;
    }

    private static PropertyInfo[] GetAccountAatRelatedProperties(GLAccount account)
    {
        if (!string.IsNullOrWhiteSpace(GlAccountAatRelatedProperties.Key) && GlAccountAatRelatedProperties.Key.Equals(nameof(GLAccount), StringComparison.OrdinalIgnoreCase))
        {
            return GlAccountAatRelatedProperties.Value;
        }
        GlAccountAatRelatedProperties = new KeyValuePair<string, PropertyInfo[]>(nameof(GLAccount), account.GetType().GetProperties());
        return account.GetType().GetProperties().Where(p => p.Name.StartsWith("AcctProjTypeCode")).ToArray();
    }

    delegate string MemberGetDelegate(GLAccount a);
}