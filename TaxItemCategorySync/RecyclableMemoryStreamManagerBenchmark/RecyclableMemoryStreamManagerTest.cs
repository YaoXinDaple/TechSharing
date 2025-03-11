using BenchmarkDotNet.Attributes;
using Microsoft.IO;

namespace TaxItemCategorySync.RecyclableMemoryStreamManagerBenchmark
{
    [MemoryDiagnoser]
    public class RecyclableMemoryStreamManagerTest
    {
        private static readonly RecyclableMemoryStreamManager Manager  = new RecyclableMemoryStreamManager();

        [Benchmark]
        public void TestMemoryStream()
        {
            for (int i = 0; i < 100_000; i++)
            {
                using (var stream = new MemoryStream())
                {
                    byte[] data = new byte[1024 * 64];
                    stream.Write(data, 0, data.Length);
                }
            }
        }

        [Benchmark(Baseline = true)]
        public void TestRecyclableMemoryStream()
        {
            for (int i = 0; i < 100_000; i++)
            {
                using (var stream = Manager.GetStream())
                {
                    byte[] data = new byte[1024 * 64];
                    stream.Write(data, 0, data.Length);
                }
            }
        }
    }
}
