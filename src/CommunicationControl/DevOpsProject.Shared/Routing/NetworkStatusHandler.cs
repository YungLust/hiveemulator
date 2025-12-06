using DevOpsProject.Shared.Grpc;
using DevOpsProject.Shared.Models;
using Listener;
using ConnectionType = DevOpsProject.Shared.Enums.ConnectionType;

namespace DevOpsProject.Shared.Routing;

public sealed class NetworkStatusHandler(IRouterService routerService) : IUdpMessageHandler<NetworkStatus>
{
    public Task HandleAsync(NetworkStatus message, CancellationToken token)
    {
        var previousConnection = routerService.GetConnectionOrNull(Connection.GetName(message.Id, (ConnectionType) message.Type));
        if (previousConnection == null)
        {
            return Task.CompletedTask;

        }

        previousConnection.IpAddress = message.IpAddress;
        previousConnection.Http1Port = message.Http1Port;
        previousConnection.GrpcPort = message.GrpcPort;
        previousConnection.UdpPort = message.UdpPort;
        previousConnection.PreviousLastUpdatedAt = previousConnection.LastUpdatedAt;
        previousConnection.LastUpdatedAt = message.SentAt.ToDateTimeOffset();
        _ = routerService.TryUpdateConnection(previousConnection, message.AliveConnectionNames.ToHashSet());
        
        return Task.CompletedTask;
    }
}
