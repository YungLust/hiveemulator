using DevOpsProject.HiveMind.Logic.Services.Interfaces;
using DevOpsProject.Shared.Grpc;
using Listener;
using Location = DevOpsProject.Shared.Models.Location;

namespace DevOpsProject.HiveMind.API.DronesTelemetryLogging;

public sealed class DroneTelemetryHandler(IDroneTelemetryService droneTelemetryService, ILogger<DroneTelemetryHandler> logger) : IUdpMessageHandler<DroneTelemetry>
{
    public Task HandleAsync(DroneTelemetry message, CancellationToken token)
    {
        var currentDroneTelemetry = droneTelemetryService.GetTelemetryModel(message.Id);
        if (currentDroneTelemetry is null)
        {
            logger.LogInformation("Drone {DroneId} was not found in telemetry service.", message.Id);
            return Task.CompletedTask;
        }
        
        currentDroneTelemetry.LastUpdatedAt = message.Timestamp.ToDateTimeOffset();
        currentDroneTelemetry.Location = new Location()
        {
            Latitude = message.Location.Latitude,
            Longitude = message.Location.Longitude
        };
        currentDroneTelemetry.Destination = message.Destination != null
            ? new Location()
            {
                Latitude = message.Destination.Latitude,
                Longitude = message.Destination.Longitude
            }
            : null;
        currentDroneTelemetry.State = (DevOpsProject.Shared.Enums.DroneState) message.State;
        currentDroneTelemetry.Speed = message.Speed;
        currentDroneTelemetry.Height = message.Height;
        currentDroneTelemetry.DroneType = (DevOpsProject.Shared.Enums.DroneType) message.DroneType;
        
        return Task.CompletedTask;
    }
}
