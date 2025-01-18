using BenchmarkDotNet.Attributes;
using System.Buffers;

namespace MemoryOptimizeExample
{
    /*
        BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4751/23H2/2023Update/SunValley3)
    12th Gen Intel Core i5-12600KF, 1 CPU, 16 logical and 10 physical cores
    .NET SDK 9.0.102
      [Host]     : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2[AttachedDebugger]
      DefaultJob : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2


    | Method                                         | Amount | Mean     | Error    | StdDev   | Gen0   | Gen1   | Allocated |
    |----------------------------------------------- |------- |---------:|---------:|---------:|-------:|-------:|----------:|
    | PeopleEmployeedWithinLocation_Classes          | 1000   | 74.47 us | 0.304 us | 0.269 us | 6.1035 | 1.0986 |  62.59 KB |
    | PeopleEmployeedWithinLocation_Structs          | 1000   | 70.93 us | 0.243 us | 0.216 us | 3.7842 |      - |  39.12 KB |
    | PeopleEmployeedWithinLocation_ArrayPoolStructs | 1000   | 70.76 us | 0.806 us | 0.754 us |      - |      - |   1.16 KB |


    当代码需要频繁操作大型缓冲区时，ArrayPool是一个很好的默认选择，与其创建一个又一个新的数组对象，不如通过ArrayPool来重用这些对象。
    */

    [MemoryDiagnoser]
    public class UsingArrayPool
    {
        [Params(1000)]
        public int Amount { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            // Warm up pool
            var array = ArrayPool<InputDataStruct>.Shared.Rent(Amount);
            ArrayPool<InputDataStruct>.Shared.Return(array);
        }

        [Benchmark]
        public List<string> PeopleEmployeedWithinLocation_Classes()
        {
            int amount = Amount;
            LocationClass location = new LocationClass();
            List<string> result = new List<string>();
            List<PersonDataClass> input = service.GetPersonsInBatchClasses(amount);
            DateTime now = DateTime.Now;
            for (int i = 0; i < input.Count; ++i)
            {
                PersonDataClass item = input[i];
                if (now.Subtract(item.BirthDate).TotalDays > 18 * 365)
                {
                    var employee = service.GetEmployeeClass(item.EmployeeId);
                    if (locationService.DistanceWithClass(location, employee.Address) < 10.0)
                    {
                        string name = string.Format("{0} {1}", item.Firstname, item.Lastname);
                        result.Add(name);
                    }
                }
            }
            return result;
        }

        [Benchmark]
        public List<string> PeopleEmployeedWithinLocation_Structs()
        {
            int amount = Amount;
            LocationStruct location = new LocationStruct();
            List<string> result = new List<string>();
            InputDataStruct[] input = service.GetPersonsInBatchStructs(amount);
            DateTime now = DateTime.Now;
            for (int i = 0; i < input.Length; ++i)
            {
                ref InputDataStruct item = ref input[i];
                if (now.Subtract(item.BirthDate).TotalDays > 18 * 365)
                {
                    var employee = service.GetEmployeeStruct(item.EmployeeId);
                    if (locationService.DistanceWithStruct(ref location, employee.Address) < 10.0)
                    {
                        string name = string.Format("{0} {1}", item.Firstname, item.Lastname);
                        result.Add(name);
                    }
                }
            }
            return result;
        }

        [Benchmark]
        public List<string> PeopleEmployeedWithinLocation_ArrayPoolStructs()
        {
            int amount = Amount;
            LocationStruct location = new LocationStruct();
            List<string> result = new List<string>();
            InputDataStruct[] input = service.GetDataArrayPoolStructs(amount);
            DateTime now = DateTime.Now;
            for (int i = 0; i < input.Length; ++i)
            {
                ref InputDataStruct item = ref input[i];
                if (now.Subtract(item.BirthDate).TotalDays > 18 * 365)
                {
                    var employee = service.GetEmployeeStruct(item.EmployeeId);
                    if (locationService.DistanceWithStruct(ref location, employee.Address) < 10.0)
                    {
                        string name = string.Format("{0} {1}", item.Firstname, item.Lastname);
                        result.Add(name);
                    }
                }
            }
            ArrayPool<InputDataStruct>.Shared.Return(input);
            return result;
        }

        public class PersonDataClass
        {
            public string Firstname;
            public string Lastname;
            public DateTime BirthDate;
            public Guid EmployeeId;
        }

        public struct InputDataStruct
        {
            public string Firstname;
            public string Lastname;
            public DateTime BirthDate;
            public Guid EmployeeId;
        }

        public class EmployeeClass
        {
            public string Name;
            public string Address;
        }
        public struct EmployeeStruct
        {
            public string Name;
            public string Address;
        }
        public class LocationClass
        {
            public double Lat;
            public double Long;
        }

        public struct LocationStruct
        {
            public double Lat;
            public double Long;
        }

        private LocationService locationService = new LocationService();
        private SomeService service = new SomeService();

        public class LocationService
        {
            internal double DistanceWithClass(LocationClass location, string address)
            {
                return 1.0;
            }

            internal double DistanceWithStruct(ref LocationStruct location, string address)
            {
                return 1.0;
            }
        }
        public class SomeService
        {
            internal List<PersonDataClass> GetPersonsInBatchClasses(int amount)
            {
                List<PersonDataClass> result = new List<PersonDataClass>(amount);
                for (int i = 0; i < amount; ++i)
                {
                    result.Add(new PersonDataClass()
                    {
                        Firstname = "A",
                        Lastname = "B",
                        BirthDate = DateTime.Now.AddYears(20),
                        EmployeeId = Guid.NewGuid()
                    });
                }
                return result;
            }

            internal InputDataStruct[] GetPersonsInBatchStructs(int amount)
            {
                InputDataStruct[] result = new InputDataStruct[amount];
                for (int i = 0; i < amount; ++i)
                {
                    result[i] = new InputDataStruct()
                    {
                        Firstname = "A",
                        Lastname = "B",
                        BirthDate = DateTime.Now.AddYears(20),
                        EmployeeId = Guid.NewGuid()
                    };
                }
                return result;
            }

            internal InputDataStruct[] GetDataArrayPoolStructs(int amount)
            {
                InputDataStruct[] result = ArrayPool<InputDataStruct>.Shared.Rent(amount);
                for (int i = 0; i < amount; ++i)
                {
                    result[i] = new InputDataStruct()
                    {
                        Firstname = "A",
                        Lastname = "B",
                        BirthDate = DateTime.Now.AddYears(20),
                        EmployeeId = Guid.NewGuid()
                    };
                }
                return result;
            }

            internal EmployeeClass GetEmployeeClass(Guid employeeId)
            {
                return new EmployeeClass() { Address = "X", Name = "Y" };
            }

            internal EmployeeStruct GetEmployeeStruct(Guid employeeId)
            {
                return new EmployeeStruct() { Address = "X", Name = "Y" };
            }
        }
    }
}
