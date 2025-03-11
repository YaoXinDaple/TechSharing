using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryOptimizeExample
{

    /*
     | Method     | Mean     | Error    | StdDev   | Allocated |
     |----------- |---------:|---------:|---------:|----------:|
     | ReadLine   | 81.28 ms | 0.719 ms | 0.638 ms |      57 B |
     | ReadColumn | 13.45 ms | 0.185 ms | 0.173 ms |       6 B |
     */
    [MemoryDiagnoser]
    public class ReadLineOrColumn
    {
        int[,] tab = new int[5000, 5000];

        [Benchmark]
        public void ReadLine()
        {
            for (int j = 0; j < 5000; j++)
            {
                for (int i = 0; i < 5000; i++)
                {
                    tab[i, j] = 1;
                }
            }
        }

        [Benchmark]
        public void ReadColumn()
        {
            for (int j = 0; j < 5000; j++)
            {
                for (int i = 0; i < 5000; i++)
                {
                    tab[j, i] = 1;
                }
            }
        }
    }
}
