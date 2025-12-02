using DevOpsProject.Shared.Grpc;
using DevOpsProject.Shared.Models;
using DevOpsProject.Shared.Routing;
using Listener;
using ConnectionType = DevOpsProject.Shared.Enums.ConnectionType;

namespace DevOpsProject.Drone.API;

public sealed class NetworkStatusHandler(IRouterService routerService) : IUdpMessageHandler<NetworkStatus>
{
    public Task HandleAsync(NetworkStatus message, CancellationToken token)
    {
        var previousConnection = routerService.GetConnectionOrNull(Connection.GetName(message.Id, (ConnectionType) message.Type));
        var connection = new Connection(message.Id, (ConnectionType) message.Type, message.IpAddress, message.Http1Port, message.GrpcPort, message.UdpPort, message.SentAt.ToDateTimeOffset())
            {
                PreviousLastUpdatedAt = previousConnection?.LastUpdatedAt ?? message.SentAt.ToDateTimeOffset()
            };
        _ = routerService.TryUpdateConnection(connection, message.AliveConnectionNames.ToHashSet());
        
        return Task.CompletedTask;
    }
}
