using Grpc.Core;
using Grpc.Net.Client;
using GrpcServerSide;

Console.WriteLine("点击任意键开始...");
Console.ReadKey();

var option = new GrpcChannelOptions
{
};
using var channel = GrpcChannel.ForAddress("https://localhost:7011", option);

var client = new GreeterServicDefinition.GreeterServicDefinitionClient(channel);


//一次调用，一次响应
//var response = client.Unary(new HelloRequest { Name = "Client" });
//Console.WriteLine(response.Message);



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
try
{
	var cts = new CancellationTokenSource();
	var call = client.ServerStream(new HelloRequest { Name = "Client" }, cancellationToken: cts.Token);
	await foreach (var response in call.ResponseStream.ReadAllAsync(cts.Token))
	{
		Console.WriteLine(response.Message);
		if (response.Message.Contains("Hello Client from GrpcServerSide,Message Title 5"))
		{
			cts.Cancel();
		}
	}
}
catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
{
    Console.WriteLine("客户端取消任务,异常捕获");
}









Console.ReadLine();