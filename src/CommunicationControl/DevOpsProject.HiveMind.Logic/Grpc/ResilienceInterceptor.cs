using Grpc.Core;
using Grpc.Core.Interceptors;
using Polly;
using Polly.Registry;

namespace DevOpsProject.HiveMind.Logic.Grpc;

public class ResilienceInterceptor : Interceptor
{
    private readonly ResiliencePipeline<object> _pipeline;

    public ResilienceInterceptor(ResiliencePipelineProvider<string> provider)
    {
        // Ideally use a specific key like "standard-retry"
        _pipeline = provider.GetPipeline<object>("grpc-retry");
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var resilienceTask = _pipeline.ExecuteAsync(async ct =>
        {
            var innerCall = continuation(request, context);
            
            var response = await innerCall.ResponseAsync;

            return new CapturedResult<TResponse>(response, innerCall);
        }).AsTask();

        var responseTask = resilienceTask.ContinueWith(t => t.Result.Response, TaskContinuationOptions.OnlyOnRanToCompletion);
        
        var headersTask = resilienceTask.ContinueWith(async t => 
        {
            var winner = t.Result.InnerCall;
            return await winner.ResponseHeadersAsync;
        }, TaskContinuationOptions.OnlyOnRanToCompletion).Unwrap();

        return new AsyncUnaryCall<TResponse>(
            responseTask,
            headersTask,
            () => resilienceTask.Result.InnerCall.GetStatus(),
            () => resilienceTask.Result.InnerCall.GetTrailers(),
            () => { }
        );
    }

    private record CapturedResult<T>(T Response, AsyncUnaryCall<T> InnerCall);
}
