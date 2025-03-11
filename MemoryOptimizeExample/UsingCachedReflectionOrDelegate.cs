using BenchmarkDotNet.Attributes;
using System.Reflection;

namespace MemoryOptimizeExample
{
    /*
    | Method                                              | Count | Mean       | Error   | StdDev   | Gen0   | Allocated |
    |---------------------------------------------------- |------ |-----------:|--------:|---------:|-------:|----------:|
    | GetAccountAatRelatedPropertiesBenchmark             | 100   |   435.9 ns | 8.66 ns | 10.31 ns | 0.0582 |     976 B |
    | GetAccountAatRelatedPropertiesWithDelegateBenchmark | 100   | 1,141.3 ns | 7.52 ns |  6.28 ns | 0.0420 |     712 B |
     */
    [MemoryDiagnoser]
    public class UsingCachedReflectionOrDelegate
    {

        /// <summary>
        /// 存储GlAccount实体中的属性信息
        /// </summary>
        public KeyValuePair<string, PropertyInfo[]> GlAccountAatRelatedProperties { get; set; }

        private delegate string MemberGetDelegate(GLAccount a);

        /// <summary>
        /// 生成100个GlAccount实体，用于测试
        /// </summary>
        [Params(100)]
        public int Count { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            //生成100个GlAccount实体，用于测试
            List<GLAccount> accounts = new List<GLAccount>();
            for (int i = 0; i < Count; i++)
            {
                //每个实体中的AcctProjectCode1到AcctProjectCode6属性在 空字符串和1-100之间随机数之间切换

                GLAccount account = new GLAccount
                {
                    AcctProjTypeCode1 = i % 2 == 0 ? "" : i.ToString(),
                    AcctProjTypeCode2 = i % 2 == 0 ? "" : i.ToString(),
                    AcctProjTypeCode3 = i % 2 == 0 ? "" : i.ToString(),
                    AcctProjTypeCode4 = i % 2 == 0 ? "" : i.ToString(),
                    AcctProjTypeCode5 = i % 2 == 0 ? "" : i.ToString(),
                    AcctProjTypeCode6 = i % 2 == 0 ? "" : i.ToString()
                };
                accounts.Add(account);
            }
        }

        [Benchmark]
        public string[] GetAccountAatRelatedPropertiesBenchmark()
        {
            GLAccount account = new GLAccount();
            var propertyInfo = GetAccountAatRelatedProperties(account);
            //从PropertyInfo中读取AcctProjectCode1到AcctProjectCode6属性
            string[] aatValues = new string[propertyInfo.Length];
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                aatValues[i] = propertyInfo[i].GetValue(account) == null ? "" : propertyInfo[i].GetValue(account)!.ToString()!;
            }
            return aatValues;
        }

        [Benchmark]
        public string[] GetAccountAatRelatedPropertiesWithDelegateBenchmark()
        {
            GLAccount account = new GLAccount();
            return GetAccountAatRelatedPropertiesWithDelegate(account);
        }


        private PropertyInfo[] GetAccountAatRelatedProperties(GLAccount account)
        {
            #region 这段代码缓存了GLAccount类型的反射结果，如果注释掉，测试100个对象需要11微秒，启用之后时间缩短为1微秒以内
            if (!string.IsNullOrWhiteSpace(GlAccountAatRelatedProperties.Key) && GlAccountAatRelatedProperties.Key.Equals(nameof(GLAccount), StringComparison.OrdinalIgnoreCase))
            {
                return GlAccountAatRelatedProperties.Value;
            }
            GlAccountAatRelatedProperties = new KeyValuePair<string, PropertyInfo[]>(nameof(GLAccount), account.GetType().GetProperties()); 
            #endregion


            return [.. account.GetType().GetProperties().Where(p => p.Name.StartsWith("AcctProjTypeCode"))];
        }

        private string[] GetAccountAatRelatedPropertiesWithDelegate(GLAccount account)
        {
            Type type = typeof(GLAccount);
            string[] aatPropertyNames = new string[] { "AcctProjTypeCode1", "AcctProjTypeCode2", "AcctProjTypeCode3", "AcctProjTypeCode4", "AcctProjTypeCode5", "AcctProjTypeCode6" };
            string[] aatValues = new string[aatPropertyNames.Length];
            for (int i = 0; i < aatPropertyNames.Length; i++)
            {
                PropertyInfo property = type.GetProperty(aatPropertyNames[i])!;
                MemberGetDelegate memberGet = (MemberGetDelegate)System.Delegate.CreateDelegate(typeof(MemberGetDelegate), property.GetGetMethod()!);
                aatValues[i] = memberGet(account);
            }
            return aatValues;
        }


    }

    internal sealed class GLAccount
    {
        /// <summary>
        /// 代码
        /// </summary>
        public string AcctCode { get; set; }

        #region AcctCode辅助解析字段
        /// <summary>
        /// 科目编码长度
        /// </summary>
        public int AcctCodeLen
        {
            get
            {
                return string.IsNullOrWhiteSpace(AcctCode) ? 0 : AcctCode.Length;
            }
        }
        /// <summary>
        /// 科目编码中是否存在某种分隔符
        /// </summary>
        public char? AcctCodeHasSeparate
        {
            get
            {
                char? separate = null;
                if (!string.IsNullOrWhiteSpace(AcctCode))
                {
                    if (AcctCode.IndexOf('.') > 0)
                    {
                        separate = '.';
                    }
                    else if (AcctCode.IndexOf('-') > 0)
                    {
                        separate = '-';
                    }
                    else if (AcctCode.IndexOf(',') > 0)
                    {
                        separate = ',';
                    }
                    //...待补充
                }
                return separate;
            }
        }
        /// <summary>
        /// 科目编码转成纯数字字符串
        /// </summary>
        public string AcctCodeAllInt
        {
            get
            {
                string acctCodeAllInt = AcctCode;
                if (AcctCodeHasSeparate.HasValue)
                {
                    acctCodeAllInt = acctCodeAllInt.Replace(AcctCodeHasSeparate.Value.ToString(), "");
                }
                return acctCodeAllInt;
            }
        }
        /// <summary>
        /// 科目编码是否纯数字
        /// </summary>
        public bool IsAcctCodeAllInt
        {
            get
            {
                Int64 code = 0;
                if (Int64.TryParse(AcctCodeAllInt, out code))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 科目编码中去除父级部分
        /// </summary>
        public string AcctSelf { get; set; }
        /// <summary>
        /// 导入系统时的新编码
        /// </summary>
        public string AcctCodeNew { get; set; }

        /// <summary>
        /// 科目类别  记账系统    1、资产类，2、负债类，3、共同类，4、所有者权益类，5、成本类，6、损益类，7、净资产类，8、收入类，9、费用类
        /// </summary>
        public int AcctCateNew
        {
            get
            {
                switch (AcctCate)
                {
                    case 1: return 1;
                    case 2: return 2;
                    case 3: return 4;
                    case 4: return 5;
                    case 5: return 6;
                    case 6: return 3;
                    default: return 0;
                }
            }
        }
        #endregion

        /// <summary>
        /// 名称
        /// </summary>
        public string AcctName { get; set; }
        /// <summary>
        /// 父级代码
        /// </summary>
        public string AcctParent { get; set; }
        /// <summary>
        /// 借贷方向    借：1  贷：-1
        /// </summary>
        public int AcctDC { get; set; }
        /// <summary>
        /// 是否末级    是：1  否：0
        /// </summary>
        public int AcctIsDetail { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int AcctLevel { get; set; }
        /// <summary>
        /// 代码全称
        /// </summary>
        public string AcctFullCode { get; set; }
        /// <summary>
        /// 名称全称
        /// </summary>
        public string AcctFullName { get; set; }
        /// <summary>
        /// 根级代码
        /// </summary>
        public string AcctRoot { get; set; }
        /// <summary>
        /// 科目类别    和导账工具客户端一致的类别 数字1到6   1资产；2负债；3权益；4成本；5损益；6共同
        /// </summary>
        public int AcctCate { get; set; }
        /// <summary>
        /// 辅助核算项目数量
        /// </summary>
        public int AcctProjCount { get; set; }
        /// <summary>
        /// 核算项目1
        /// </summary>
        public string AcctProjTypeCode1 { get; set; }
        /// <summary>
        /// 核算项目2
        /// </summary>
        public string AcctProjTypeCode2 { get; set; }
        /// <summary>
        /// 核算项目3
        /// </summary>
        public string AcctProjTypeCode3 { get; set; }
        /// <summary>
        /// 核算项目4
        /// </summary>
        public string AcctProjTypeCode4 { get; set; }
        /// <summary>
        /// 核算项目5
        /// </summary>
        public string AcctProjTypeCode5 { get; set; }
        /// <summary>
        /// 核算项目6
        /// </summary>
        public string AcctProjTypeCode6 { get; set; }
        /// <summary>
        /// 是否期末调汇  是：1  否：0
        /// </summary>
        public int FX_SFQMTH { get; set; }
        /// <summary>
        /// 是否数量核算  是：1  否：0
        /// </summary>
        public int FX_SFSLHS { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string FX_SLHSUnit { get; set; }
        /// <summary>
        /// 币种代码
        /// </summary>
        public string FX_WBBZ { get; set; }

        /// <summary>
        /// 科目启用禁用状态  启用：0  禁用：1
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 非必录辅助核算类型
        /// </summary>
        public string AllowNullAATypes { get; set; }
    }
}
