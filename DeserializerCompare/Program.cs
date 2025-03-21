﻿// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Comparer;
using Google.Protobuf;
using Newtonsoft.Json;
using System.Text;
using ProtoBuf;
using System;

Console.WriteLine("Hello, World!");

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
//var summary = BenchmarkRunner.Run<DeserializationBenchmarks>();

[MemoryDiagnoser]
public class DeserializationBenchmarks
{
    private byte[] protobufData;
    private readonly string jsonData;
    private readonly Person person = new Person
    {
        Name = "John Doe",
        Id = 1234,
        Email = "johndoe@example.com"
    };

    public DeserializationBenchmarks()
    {
        // 初始化 Protobuf 数据
        using (var ms = new MemoryStream())
        {
            person.WriteTo(ms);

            // 将 MemoryStream 转换为字节数组
            protobufData = ms.ToArray();

            // 输出序列化后的字节数组长度
            Console.WriteLine($"Serialized data length: {protobufData.Length}");
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

[MemoryDiagnoser]
public class SerializationBenchmarks
{
    private readonly Person person = new Person
    {
        Name = "John Doe",
        Id = 1234,
        Email = "johndoe@example.com"
    };

    [Benchmark]
    public byte[] ProtobufSerialization()
    {
        using (var ms = new MemoryStream())
        {
            person.WriteTo(ms);
            return ms.ToArray();
        }
    }

    [Benchmark]
    public byte[] JsonSerialization()
    {
        //return JsonConvert.SerializeObject(person);

        return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(person));
    }
}