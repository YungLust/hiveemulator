using System.Collections.Concurrent;
using DevOpsProject.HiveMind.Logic.Models;
using DevOpsProject.HiveMind.Logic.Services.Interfaces;
using DevOpsProject.Shared.Configuration;
using DevOpsProject.Shared.Enums;
using DevOpsProject.Shared.Models;
using DevOpsProject.Shared.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DevOpsProject.HiveMind.Logic.Services;

public sealed class DroneTelemetryService(IRouterService routerService, ILogger<DroneTelemetryService> logger, IOptionsSnapshot<HiveCommunicationConfig> communicationConfigurationOptions) : IDroneTelemetryService
{
    private readonly ConcurrentDictionary<string, DroneTelemetryModel> _drones = new();

    public bool Add(DroneTelemetryModel model)
    {
        return _drones.TryAdd(model.Id, model);
    }

    public DroneTelemetryModel GetTelemetryModel(string droneId)
    {
        return _drones[droneId];
    }
    
    public bool Remove(string droneId)
    {
        return _drones.TryRemove(droneId, out _);
    }

    public void LogTelemetry()
    {
        var currentTime = DateTimeOffset.UtcNow;

        var hiveMindConnections = routerService.GetConnectedDevicesNames(Connection.GetName(communicationConfigurationOptions.Value.HiveID, ConnectionType.Hive));
        logger.LogInformation("[{Timestamp}] HiveMind {Id} Connections: {ConnectionsNames}", 
            currentTime, 
            communicationConfigurationOptions.Value.HiveID,
            string.Join(", ", hiveMindConnections));
        
        var dronesIds = _drones.Keys.ToList();
        foreach (var droneId in dronesIds)
        {
            var drone = _drones[droneId];
            var connection = routerService.GetConnectionOrNull(Connection.GetName(droneId, ConnectionType.Drone));
            var connectedDevices =
                routerService.GetConnectedDevicesNames(Connection.GetName(droneId, ConnectionType.Drone));
            if (connection is null)
            {
                logger.LogWarning("[{Timestamp}] No connection found for {DroneId}.", currentTime, droneId);
                continue;
            }
            
            logger.LogInformation("[{Timestamp}] {DroneId}: {ConnectionStatus} {State} Location: ({LocationLat},{LocationLon}) Destination: ({DestinationLat},{DestinationLon})", 
                currentTime, 
                droneId,
                connection.State,
                drone.State,
                drone.Location.Latitude,
                drone.Location.Longitude,
                drone.Destination?.Latitude,
                drone.Destination?.Longitude);
            logger.LogInformation("[{TimeStamp}] {DroneId}: Connections: {ConnectionsNames}", currentTime, droneId, string.Join(", ", connectedDevices));
        }
    }
}
