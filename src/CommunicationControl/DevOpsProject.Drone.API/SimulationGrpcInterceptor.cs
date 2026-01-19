using DevOpsProject.Shared.Routing;
using DevOpsProject.Shared.Simulation;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace DevOpsProject.Drone.API;

public sealed class SimulationGrpcInterceptor(ISimulationUtility simulationUtility, ILogger<SimulationGrpcInterceptor> logger) : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var previousHopHeader = context.RequestHeaders.FirstOrDefault(h => h.Key == RoutingConstants.PreviousHopHeaderName);
        if (previousHopHeader != null)
        {
            var deviceSimulation = simulationUtility.BadDevice;
            if (deviceSimulation != null)
            {
                logger.LogWarning("Simulation - drone delay {SimulationLatency}.", deviceSimulation.Latency);
                await Task.Delay(deviceSimulation.Latency, deviceSimulation.CancellationToken);
            }
            
            var connectionSimulationLatency = simulationUtility.GetBadConnection(previousHopHeader.Value);
            if (connectionSimulationLatency != null)
            {
                logger.LogWarning("Simulation - connection delay {ConnectionSimulationLatency}.", connectionSimulationLatency.Latency);
                await Task.Delay(connectionSimulationLatency.Latency, connectionSimulationLatency.CancellationToken);
            }
        }
        
        return await continuation(request, context);
    }
}
