using DevOpsProject.Shared.Enums;

namespace DevOpsProject.Shared.Models;

public sealed class Connection
{
    public string Name => GetName(DeviceId, Type);
    public string DeviceId { get; }
    public ConnectionType Type { get; }
    public Uri Http1Uri { get; }
    public Uri GrpcUri { get; }
    public Uri UdpUri { get; }
    public ConnectionState State { get; set; } = ConnectionState.Alive;

    public Connection(string deviceId, ConnectionType type, string ipAddress, ushort http1Port, ushort grpcPort, ushort udpPort) 
    {
        DeviceId = deviceId;
        Type = type;
        Http1Uri = GetUri(ipAddress, http1Port);
        GrpcUri = GetUri(ipAddress, grpcPort);
        UdpUri = GetUri(ipAddress, udpPort);
    }

    private Uri GetUri(string ipAddress, ushort port)
    {
        var builder = new UriBuilder(ipAddress)
        {
            Port = port
        };
        return builder.Uri;
    }
    
    public Connection(string deviceId, ConnectionType type, Uri http1Uri, Uri grpcUri, Uri udpUri)
    {
        DeviceId = deviceId;
        Type = type;
        Http1Uri = http1Uri;
        GrpcUri = grpcUri;
        UdpUri = udpUri;
    }

    public static string GetName(string deviceId, ConnectionType type)
        => $"{type.ToString().ToLower()}:{deviceId}";
}
