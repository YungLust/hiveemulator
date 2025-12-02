using DevOpsProject.Drone.Logic.Services.Interfaces;
using DevOpsProject.Drone.Logic.State;

namespace DevOpsProject.Drone.Logic.Services;

public sealed class DroneService(IDroneState droneState) : IDroneService, IDisposable
{
    private Timer? _movementTimer;
    
    // TODO: Move()
    // TODO: StopMoving()
    
    /*
     *
     *     public void Move(float stepSize)
    {
        if (State != Shared.Enums.DroneState.Move)
        {
            throw new InvalidOperationException("Cannot move in non-movable state");
        }

        lock (_movementLock)
        {
            _location = CalculateNextPosition(stepSize);
            
            if (AreLocationsEqual(_location, Destination))
            {
                _state = Shared.Enums.DroneState.Stop;
            }
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
        lock (_movementLock)
        {
            var newLat = _location.Latitude + (Destination.Latitude - _location.Latitude) * stepSize;
            var newLon = _location.Longitude + (Destination.Longitude - _location.Longitude) * stepSize;
            return new Location
            {
                Latitude = newLat,
                Longitude = newLon
            };
        }
    }
     * 
     */
    
    public void Dispose()
    {
        _movementTimer?.Dispose();
        _movementTimer = null;
    }
}
