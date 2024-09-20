using Grpc.Core;
using Grpc.Core.Interceptors;

namespace GrpcServerSide.Interceptors
{
    public class ServerLogginInterceptor:Interceptor
    {
        private readonly ILogger<ServerLogginInterceptor> _logger;

        public ServerLogginInterceptor(ILogger<ServerLogginInterceptor> logger)
        {
            _logger = logger;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            try
            {
                _logger.LogInformation($"starting the server call of type:{context.Method.FullName},{context.Method.Type}");
                return continuation(request, context);
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            try
            {
                _logger.LogInformation($"starting the server call of type:{context.Method.FullName},{context.Method.Type}");
                return continuation(request, context);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                _logger.LogInformation($"starting interceptor here!");
                return continuation(request, context);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
