using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Compression;
using GrpcServerSide;
using Microsoft.Extensions.Logging;

//开启日志
// 创建日志工厂
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.SetMinimumLevel(LogLevel.Trace); // 设置最低日志级别
});

// 创建日志记录器
var logger = loggerFactory.CreateLogger<Program>();

while (true)
{

    Console.WriteLine("点击任意键开始...");
    Console.ReadKey();

    var option = new GrpcChannelOptions
    {
        CompressionProviders = new List<ICompressionProvider>
               {
                   new GzipCompressionProvider(System.IO.Compression.CompressionLevel.SmallestSize)
               }
    };

    using var channel = GrpcChannel.ForAddress("https://localhost:7011" , option);

    using var channel = GrpcChannel.ForAddress("https://localhost:7011"
        , new GrpcChannelOptions { CompressionProviders = new List<ICompressionProvider>() }
        );

    var client = new GreeterServicDefinition.GreeterServicDefinitionClient(channel);


    //var metadata = new Metadata { { "grpc-accept-encoding", "gzip" }, { "accept-encoding", "gzip" } };



    var metadata = new Metadata
    {
        { "grpc-accept-encoding", "gzip" },
        { "accept-encoding", "gzip" }
    };
    var callOptions = new Grpc.Core.CallOptions(metadata);

    //一次调用，一次响应
    var response = client.Unary(new HelloRequest { Name = "Client" });
    Console.WriteLine(response.Message);

}


//客户端，多次调用，一次返回
//using var call = client.ClientStream();
//for (int i = 0; i < 10; i++)
//{
//    await call.RequestStream.WriteAsync(new HelloRequest { Name = "Client" + i });
//}
//await call.RequestStream.CompleteAsync();
//HelloReply response = await call;
//Console.WriteLine(response.Message);



//客户端，一次调用，服务端多次返回
//var call = client.ServerStream(new HelloRequest { Name = "Client" });
//await foreach (var response in call.ResponseStream.ReadAllAsync())
//{
//    Console.WriteLine(response.Message);
//}


//客户端多次调用，服务端多次返回
//var call = client.BiDirectionalStream();

//var sendTask =  SendMessages(call.RequestStream);
//var receiveTask = ReceiveMessages(call.ResponseStream);

//await sendTask;
//await call.RequestStream.CompleteAsync();

//await receiveTask;

//async static Task SendMessages(IAsyncStreamWriter<HelloRequest> requestStream)
//{
//    for (int i = 0; i < 10; i++)
//    {
//        await Task.Delay(1000); // 使用 Task.Delay 替代 Thread.Sleep
//        Console.WriteLine("客户端发出请求" + i);
//        await requestStream.WriteAsync(new HelloRequest { Name = "Client" + i });
//    }
//}

//async static Task ReceiveMessages(IAsyncStreamReader<HelloReply> responseStream)
//{
//    while (await responseStream.MoveNext())
//    {
//        var currentResponse = responseStream.Current;
//        Console.WriteLine(currentResponse.Message);
//    }
//}



//客户端接收到特定结果时，取消调用
//try
//{
//	var cts = new CancellationTokenSource();
//	var call = client.ServerStream(new HelloRequest { Name = "Client" }, cancellationToken: cts.Token);
//	await foreach (var response in call.ResponseStream.ReadAllAsync(cts.Token))
//	{
//		Console.WriteLine(response.Message);
//		if (response.Message.Contains("Hello Client from GrpcServerSide,Message Title 5"))
//		{
//			cts.Cancel();
//		}
//	}
//}
//catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
//{
//    Console.WriteLine("客户端取消任务,异常捕获");
//}









Console.ReadLine();