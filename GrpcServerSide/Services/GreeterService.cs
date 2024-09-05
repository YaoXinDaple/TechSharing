using Grpc.Core;

namespace GrpcServerSide.Services
{
    public class GreeterService : GreeterServicDefinition.GreeterServicDefinitionBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> Unary(HelloRequest request, ServerCallContext context)
        {
            var reponse = new HelloReply
            {
                Message = "Hello " + request.Name + " from GrpcServerSide"
            };
            return Task.FromResult(reponse);
        }

        public override async Task<HelloReply> ClientStream(IAsyncStreamReader<HelloRequest> requestStream, ServerCallContext context)
        {
            var names = new List<string>();
            await foreach (var request in requestStream.ReadAllAsync())
            {
                names.Add(request.Name);
            }
            var reponse = new HelloReply
            {
                Message = "Hello " + string.Join(",", names) + " from GrpcServerSide"
            };
            return reponse;
        }

        public override async Task ServerStream(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    if (context.CancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("�ͻ���ȡ������");
                        await Task.FromCanceled(context.CancellationToken);
                    }
                    await Task.Delay(500);
                    Console.WriteLine("����˿�ʼ��ͻ��˷�����Ϣ��" + i);
                    await responseStream.WriteAsync(new HelloReply
                    {
                        Message = "Hello " + request.Name + " from GrpcServerSide,Message Title " + i
                    }, context.CancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("�ͻ���������ȡ��");
            }
        }

        public override async Task BiDirectionalStream(IAsyncStreamReader<HelloRequest> requestStream, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var requestPayload = requestStream.Current;
                Console.WriteLine("���յ��ͻ��˴�����Ϣ��" + requestPayload.Name);
                await Task.Delay(1000);
                Console.WriteLine("����˷�����Ϣ��Hello " + requestPayload.Name + " from GrpcServerSide");
                Task t1 = NameTask(requestPayload.Name, responseStream);
            }
        }

        private async Task NameTask(string name, IServerStreamWriter<HelloReply> responseStream)
        {
            if (name=="1")
            {
                await Task.Delay(10000);
            }
            else if(name=="2")
            {
                await Task.Delay(2000);
            }
            await responseStream.WriteAsync(new HelloReply
            {
                Message = "Hello " + name + " from GrpcServerSide"
            });
        }


    }
}
