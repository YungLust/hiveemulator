using DevOpsProject.HiveMind.Logic.Grpc;
using DevOpsProject.HiveMind.Logic.Services.Interfaces;
using DevOpsProject.Shared.Grpc;
using Grpc.Core.Interceptors;

namespace DevOpsProject.HiveMind.Logic.Services;

public sealed class DroneService(IGrpcChannelFactory grpcChannelFactory, ResilienceInterceptor grpcInterceptor) : IDroneService
{
    public async Task ConnectDroneAsync(string ipAddress, int port)
    {
        var uriBuilder = new UriBuilder(ipAddress);
        var channel = grpcChannelFactory.Create(new Uri(uriBuilder.ToString()));
        var callInvoker = channel.Intercept(grpcInterceptor);
        var client = new Shared.Grpc.DroneService.DroneServiceClient(callInvoker);
    }
}
