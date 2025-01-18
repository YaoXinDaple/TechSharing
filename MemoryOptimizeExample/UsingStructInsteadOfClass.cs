using BenchmarkDotNet.Attributes;
using System.Buffers;
using System.Globalization;

namespace MemoryOptimizeExample
{
    /*
        BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4751/23H2/2023Update/SunValley3)
        12th Gen Intel Core i5-12600KF, 1 CPU, 16 logical and 10 physical cores
        .NET SDK 9.0.102
          [Host]     : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2 [AttachedDebugger]
          DefaultJob : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2


        | Method            | Mean     | Error   | StdDev  | Gen0   | Allocated |
        |------------------ |---------:|--------:|--------:|-------:|----------:|
        | ProcessStackalloc | 258.1 ns | 1.68 ns | 1.57 ns |      - |         - |
        | ProcessEnumerable | 499.1 ns | 4.49 ns | 3.75 ns | 0.3128 |    3272 B |

    使用struct相对于class的优势：
        需要处理的数据越少越好，struct不包含其他任何额外数据。
        可以充分利用缓存（这里指的于CPU缓存，CPU从内存中读取数据时不是一次只读取实际数据所占用的空间，而是按照cache line 的方式，每次读取一整块内存，并放入CPU缓存中），由于struct类型的数组是由连续的内存块组成,
    而class类型只包含连续的引用，引用所指向的实际值分散在整个托管堆中，我们对此无能为力。
    
     使用stackalloc所定义对象占用的栈空间会在方法执行完毕后自动释放，但是要注意stakcalloc只能用于少量数据（例如少于1kb），一旦导致StackOverflowException，会导致整个程序崩溃，没有恢复的机会

     */
    [MemoryDiagnoser]
    public class UsingStructInsteadOfClass
    {
        private Logger _logger;
        private List<BigData> _list;
        int _items = 100;

        [GlobalSetup]
        public void Setup()
        {
            _list = new List<BigData>();
            for (int i = 0; i < _items; i++)
            {
                _list.Add(new BigData() { Age = i, Description = "SOME_FEM_DATA" });
            }
            // Warm up StringBuilderCache
            Console.WriteLine(string.Format("Warming: {0}", _items.ToString(CultureInfo.InvariantCulture)));
            _logger = new Logger
            {
                Level = Logger.LoggingLevel.Info
            };
            Span<int> var = ArrayPool<int>.Shared.Rent(100);
        }

        [Benchmark]
        public double ProcessStackalloc()
        {
            //DataStruct[] data = _list.Select(x => new DataStruct() {Age = x.Age, Sex = x.Sex}).ToArray(); // to nadal na heapie tyle że mniejsze
            Span<DataStruct> data = stackalloc DataStruct[_list.Count];
            for (int i = 0; i < _list.Count; ++i)
            {
                data[i].Age = _list[i].Age;
                data[i].Sex = Helper(_list[i].Description) ? Sex.Female : Sex.Male;
            }
            double avg = ProcessData2(data);
            _logger.Debug("Result: {0}", avg / _items);
            return avg;
        }

        [Benchmark]
        public double ProcessEnumerable()
        {
            double avg = ProcessData1(_list.Select(x => new DataClass()
            {
                Age = x.Age,
                Sex = Helper(x.Description) ? Sex.Female : Sex.Male //x.Description.Substring(5, 3) == "FEM" ? Sex.Female : Sex.Male
            }));
            _logger.Debug("Result: {0}", avg / _items);
            return avg;
        }

        public double ProcessData1(IEnumerable<DataClass> list)
        {
            double sum = 0;
            foreach (var x in list)
            {
                sum += x.Age;
            }
            return sum;
        }

        public double ProcessData2(ReadOnlySpan<DataStruct> list)
        {
            double sum = 0;
            for (int i = 0; i < list.Length; ++i)
            {
                sum += list[i].Age;
            }
            return sum;
        }
        private static bool Helper(string str)
        {
            return str[5] == 'F' && str[6] == 'E' && str[7] == 'M';
        }
    }

    public class BigData
    {
        public string Description;
        public double Age;
    }

    public class DataClass
    {
        public Sex Sex;
        public double Age;
    }

    public struct DataStruct
    {
        public Sex Sex;
        public double Age;
    }


    public enum Sex
    {
        Male,
        Female
    }

    public class Logger
    {
        public enum LoggingLevel
        {
            Debug,
            Info,
            Warning
        }

        public LoggingLevel Level { get; set; }

        private void Log(LoggingLevel level, string message)
        {
            if (level >= Level)
            {
                Console.WriteLine(message);
            }
        }
        private void Log(LoggingLevel level, Func<string> exceptionMessage)
        {
            if (level >= Level)
            {
                Console.WriteLine(exceptionMessage());
            }
        }
        private void Log<T>(LoggingLevel level, string format, T arg)
        {
            if (level >= Level)
            {
                Console.WriteLine(string.Format(format, arg));
            }
        }

        public void Debug(string message) => Log(LoggingLevel.Debug, message);
        public void Info(string message) => Log(LoggingLevel.Info, message);
        public void Warning(string message) => Log(LoggingLevel.Warning, message);
        public void Debug(Func<string> exceptionMessage) => Log(LoggingLevel.Debug, exceptionMessage);
        public void Info(Func<string> exceptionMessage) => Log(LoggingLevel.Info, exceptionMessage);
        public void Warning(Func<string> exceptionMessage) => Log(LoggingLevel.Warning, exceptionMessage);
        public void Debug<T>(string format, T arg) => Log(LoggingLevel.Debug, format, arg);
        public void Info<T>(string format, T arg) => Log(LoggingLevel.Info, format, arg);
        public void Warning<T>(string format, T arg) => Log(LoggingLevel.Warning, format, arg);
    }
}
