## gRpc

### 1. What is gRpc?
- gRPC is a high performance, open-source universal RPC framework(�����ܣ���ƽ̨)
- gRPC was developed by Google and is based on the HTTP/2 protocol������Http2Э�飬��·���ã�


### 2. What is Protocol Buffers?
- Protocol Buffers is a method developed by Google to serialize structured data
- Protocol Buffers is used to serialize structured data into a binary format�������ƣ������л����ܱ�json�ߣ�


## HTTP/2
	˵��Http/2����������Ҫ�˽�һ��Http/1.1�����⣬Http/1.1��������Ҫ�У�
	1. ���е����󣬵�����������
	2. ����ͷ���࣬���´���Ч�ʵ�

���������Դͷ����HttpЭ���ǻ���TCPЭ��ģ���TCPЭ����һ���������ӵ�Э�飬������һ��������ֻ�ܷ���һ��������͵����˴�������

	Http/1.1���Http/1.0�ĸĽ���
	1. ֧�ֳ����� => keep-alive

�����ӵĺô��ǿ��Լ������ӵĽ����ͶϿ��Ŀ��������ǳ�������Ȼû�н��������������⡣

	Http/2���ص㣺
	1. ��·����
	2. ͷ��ѹ��
	3. ֧�ַ���������/�ͻ�������/˫������

��Http/2ͨ����·���ý���˴�����������⣬��·����������һ��������ͬʱ���Ͷ������
Http/2�е���������������ͨ����·���ý���ģ���·����������һ��������ͬʱ���Ͷ�����������ͱ������������������⡣
HTTP/2�У���Ȼͨ����·���ü��������Ӧ�ò���Ķ�ͷ�������⣬����TCP������Ȼ���ڶ�ͷ������

	Http/3
Http/3�ǻ���QUICЭ��ģ�QUICЭ���ǻ���UDPЭ��� ����֮ǰ��HTTPЭ�鶼�ǻ���TCPЭ��ġ�
QUICЭ����Խ��TCPЭ���һЩ���⣬����˵���ӵĽ����ͶϿ��Ŀ������Լ����ӵ��������⡣
�������ļ��ϴ������У����绷���л�ʱ��TCPЭ��ᵼ���ļ��ϴ�ʧ�ܣ���QUIC֧�ֿ������ӽ����������ڵ���������������ӽ�������ԿЭ�̣������ڵڶ�������ʱʵ��0-RTT��������ʱ�䣩�����ݴ���

## gRpc������
Windows10��֮ǰ��ϵͳ,����ʱʹ��IIS Express����֧������gRpc����
	��Ҫ�޸�ApplicationBuilder.UseKestrel()������

```csharp
builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ConfigureEndpointDefaults(listenOptions =>
        {
            listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
        });
    });
```

Windows Server 2012(֮ǰ?)������������

## ����gRpc Client
	�鿴Consumer��Ŀ�ļ�

## �����л����ܻ�׼���Խ��
	DeserializerCompare��Ŀ

## ����gRpc���񽻻���ʽ
	Unary 
	Client Streaming
	Server Streaming
	Bidirectional Streaming

## ����������Post����

## ��Ӧ��ѹ��
	1. �����ָ��ѹ���㷨
	2. �ͻ���Ĭ��֧��gzipѹ��

## Channel����
	Channel������һ������Ĳ���������Ӧ������Channel
	1. gRpcClientFactory
	https://learn.microsoft.com/en-us/aspnet/core/grpc/clientfactory?view=aspnetcore-6.0
	
	2.// ע�ᵥ���� GrpcChannel
    var channel = GrpcChannel.ForAddress("https://localhost:5001");
    services.AddSingleton(channel);

    // ע�� gRPC �ͻ��ˣ�����ͬһ�� channel
    services.AddScoped(greeterClient => new Greeter.GreeterClient(channel));
    services.AddScoped(anotherClient => new AnotherService.AnotherServiceClient(channel));

## ������ Interceptor



## StackTrace
����Ŀ����ʱ����ȡ���ö�ջ��Ϣ����λ����һ�ε��ó��ֵ�����

�鿴GetStackTrace��Ŀ
## FrozenDictionary
## AsSpan(StringConstructor��Ŀ)