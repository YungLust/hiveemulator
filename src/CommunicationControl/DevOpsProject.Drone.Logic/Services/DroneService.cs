using DevOpsProject.Drone.Logic.Services.Interfaces;
using DevOpsProject.Drone.Logic.State;
using DevOpsProject.Shared.Models;

namespace DevOpsProject.Drone.Logic.Services;

public sealed class DroneService(IDroneState droneState) : IDroneService, IDisposable
{
    private Timer? _movementTimer;
    
    // TODO: StartMoving()
    // TODO: StopMoving()
    
    private void Move(float stepSize)
    {
        if (droneState.State != Shared.Enums.DroneState.Moving)
        {
            throw new InvalidOperationException("Cannot move in non-movable state");
        }
        
        droneState.Location = CalculateNextPosition(stepSize);
            
        if (AreLocationsEqual(droneState.Location, droneState.Destination))
        {
            droneState.State = Shared.Enums.DroneState.Static;
        }
    }
    
    private static bool AreLocationsEqual(Location loc1, Location loc2)
    {
        const float tolerance = 0.000001f;
        return Math.Abs(loc1.Latitude - loc2.Latitude) < tolerance &&
               Math.Abs(loc1.Longitude - loc2.Longitude) < tolerance;
    }

    private Location CalculateNextPosition(float stepSize)
    {
        var newLat = droneState.Location.Latitude +
                     (droneState.Destination.Latitude - droneState.Location.Latitude) * stepSize;
        var newLon = droneState.Location.Longitude +
                     (droneState.Destination.Longitude - droneState.Location.Longitude) * stepSize;
        return new Location
        {
            Latitude = newLat,
            Longitude = newLon
        };
    }
    
    public void Dispose()
    {
        _movementTimer?.Dispose();
        _movementTimer = null;
    }
}
