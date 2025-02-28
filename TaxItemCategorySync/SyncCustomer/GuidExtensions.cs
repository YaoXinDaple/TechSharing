using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxItemCategorySync.SyncCustomer
{
    public static class GuidExtensions
    {
        public static bool IsNotNullOrEmpty([NotNullWhen(true)] this Guid? guid)
        {
            return guid.HasValue && guid.Value.IsNotNullOrEmpty();
        }

        /// <summary>
        /// 判断Guid值是否为空
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this Guid guid)
        {
            return guid != Guid.Empty;
        }

        public static bool IsNullOrEmpty([NotNullWhen(false)] this Guid? guid)
        {
            return !guid.IsNotNullOrEmpty();
        }

        public static bool IsNullOrEmpty(this Guid guid)
        {
            return !guid.IsNotNullOrEmpty();
        }
        public static int CompareSequentialGuids(Guid guid1, Guid guid2)
        {
            // 提取时间戳部分（反转后的前6个字节）
            byte[] timestampBytes1 = new byte[6];
            byte[] timestampBytes2 = new byte[6];
            var b1 = guid1.ToByteArray();
            var b2 = guid2.ToByteArray();

            //取二进制数组的后6个字节
            b1.AsSpan().Slice(b1.Length - 6).CopyTo(timestampBytes1);
            b2.AsSpan().Slice(b1.Length - 6).CopyTo(timestampBytes2);

            // 逐字节比较时间戳
            for (int i = 0; i < 6; i++)
            {
                if (timestampBytes1[i] < timestampBytes2[i])
                    return -1;  // guid1 小于 guid2
                if (timestampBytes1[i] > timestampBytes2[i])
                    return 1;   // guid1 大于 guid2
            }

            return 0; // guid1 和 guid2 相等
        }
    }
}
