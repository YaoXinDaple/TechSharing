using GrpcServerSide.Interceptors;
using GrpcServerSide.Services;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureEndpointDefaults(listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
    });
});

builder.Logging.ClearProviders(); // 清除所有默认日志提供程序
builder.Logging.AddConsole()
       .AddFilter("Microsoft.AspNetCore", LogLevel.Trace)
       .AddFilter("System", LogLevel.Trace)
       .AddFilter("Default", LogLevel.Trace);


builder.Services.AddTransient<ServerLogginInterceptor>();
// Add services to the container.
builder.Services.AddGrpc(options =>
{
    //设置服务端压缩算法
    options.ResponseCompressionAlgorithm = "gzip";
    options.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.SmallestSize;
    options.Interceptors.Add<ServerLogginInterceptor>();
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    //options.Providers.Add<BrotliCompressionProvider>();
    //options.Providers.Add<GzipCompressionProvider>();
});

var app = builder.Build();
//app.UseResponseCompression();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
