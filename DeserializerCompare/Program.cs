// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Comparer;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
//var summary = BenchmarkRunner.Run<SerializationBenchmarks>();

[SimpleJob(launchCount: 1, warmupCount: 1, iterationCount: 1)]
[MemoryDiagnoser]
public class SerializationBenchmarks
{
    private readonly byte[] protobufData;
    private readonly string jsonData;
    private readonly Person person = new Person
    {
        Name = "John Doe",
        Id = 1234,
        Email = "johndoe@example.com"
    };

    public SerializationBenchmarks()
    {
        // 初始化 Protobuf 数据
        using (var ms = new MemoryStream())
        {
            using (var cos = new Google.Protobuf.CodedOutputStream(ms))
            {
                person.WriteTo(cos);
                cos.Flush(); // 确保所有数据都已写入到 MemoryStream
            }
            protobufData = ms.ToArray();
        }


        // 初始化 JSON 数据
        jsonData = JsonConvert.SerializeObject(person);
    }

    [Benchmark]
    public Person ProtobufDeserialization()
    {
        return Person.Parser.ParseFrom(protobufData);
    }

    [Benchmark]
    public Person JsonDeserialization()
    {
        return JsonConvert.DeserializeObject<Person>(jsonData)!;
    }
}