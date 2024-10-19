## gRpc

### 1. What is gRpc?
- gRPC is a high performance, open-source universal RPC framework(高性能，跨平台)
- gRPC was developed by Google and is based on the HTTP/2 protocol（基于Http2协议，多路复用）


### 2. What is Protocol Buffers?
- Protocol Buffers is a method developed by Google to serialize structured data
- Protocol Buffers is used to serialize structured data into a binary format（二进制，反序列化性能比json高）


## HTTP/2
	说到Http/2，我们首先要了解一下Http/1.1的问题，Http/1.1的问题主要有：
	1. 串行的请求，导致请求阻塞
	2. 请求头冗余，导致传输效率低

串行请求的源头在于Http协议是基于TCP协议的，而TCP协议是一个面向连接的协议，所以在一个连接上只能发送一个请求，这就导致了串行请求。

	Http/1.1相比Http/1.0的改进：
	1. 支持长连接 => keep-alive

长连接的好处是可以减少连接的建立和断开的开销，但是长连接仍然没有解决串行请求的问题。

	Http/2的特点：
	1. 多路复用
	2. 头部压缩
	3. 支持服务器推送/客户端推送/双向推送

而Http/2通过多路复用解决了串行请求的问题，多路复用允许在一个连接上同时发送多个请求。
Http/2中的连接阻塞问题是通过多路复用解决的，多路复用允许在一个连接上同时发送多个请求，这样就避免了请求阻塞的问题。
HTTP/2中，虽然通过多路复用技术解决了应用层面的队头阻塞问题，但在TCP层面仍然存在队头阻塞。

	Http/3
Http/3是基于QUIC协议的，QUIC协议是基于UDP协议的 ，而之前的HTTP协议都是基于TCP协议的。
QUIC协议可以解决TCP协议的一些问题，比如说连接的建立和断开的开销，以及连接的阻塞问题。
另外在文件上传过程中，网络环境切换时，TCP协议会导致文件上传失败，但QUIC支持快速连接建立，可以在单个往返中完成连接建立和密钥协商，甚至在第二次连接时实现0-RTT（零往返时间）的数据传输

## gRpc兼容性
Windows10及之前的系统,开发时使用IIS Express，不支持运行gRpc服务
	需要修改ApplicationBuilder.UseKestrel()的配置

```csharp
builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ConfigureEndpointDefaults(listenOptions =>
        {
            listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
        });
    });
```

Windows Server 2012(之前?)可以正常运行

## 设置gRpc Client
	查看Consumer项目文件

## 反序列化性能基准测试结果
	DeserializerCompare项目

## 四种gRpc服务交互方式
	Unary 
	Client Streaming
	Server Streaming
	Bidirectional Streaming

## 所有请求都是Post请求

## 响应体压缩
	1. 服务端指定压缩算法
	2. 客户端默认支持gzip压缩

## Channel复用
	Channel创建是一个昂贵的操作，所以应当复用Channel
	1. gRpcClientFactory
	https://learn.microsoft.com/en-us/aspnet/core/grpc/clientfactory?view=aspnetcore-6.0
	
	2.// 注册单例的 GrpcChannel
    var channel = GrpcChannel.ForAddress("https://localhost:5001");
    services.AddSingleton(channel);

    // 注册 gRPC 客户端，复用同一个 channel
    services.AddScoped(greeterClient => new Greeter.GreeterClient(channel));
    services.AddScoped(anotherClient => new AnotherService.AnotherServiceClient(channel));

## 拦截器 Interceptor



## StackTrace
在项目调试时，获取调用堆栈信息，定位是哪一次调用出现的问题

查看GetStackTrace项目
## FrozenDictionary
## AsSpan(StringConstructor项目)