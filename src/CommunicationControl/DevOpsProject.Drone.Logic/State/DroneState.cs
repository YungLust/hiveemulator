using DevOpsProject.Shared.Enums;
using DevOpsProject.Shared.Models;
using Microsoft.Extensions.Options;

namespace DevOpsProject.Drone.Logic.State;

public sealed class DroneState : IDroneState
{
    public DroneState(IOptions<DroneInitialStateOptions> options)
    {
        DroneId = options.Value.DroneId;
        Location = options.Value.Location;
    }
    
    public string Name => Connection.GetName(DroneId, ConnectionType.Drone);
    public string DroneId { get; set; }
    public Location Location { get; set; }

    public Location Destination
    {
        get;
        set
        {
            if (!AreLocationsEqual(value, field))
            {
                State = Shared.Enums.DroneState.Move;
            }

            field = value;
        }
    }

    public float Speed { get; set; }
    public float Height { get; set; }
    public DevOpsProject.Shared.Enums.DroneState State { get; set; }

    public void Move(float stepSize)
    {
        if (State != Shared.Enums.DroneState.Move)
        {
            throw new InvalidOperationException("Cannot move in non-movable state");
        }
        
        Location = CalculateNextPosition(stepSize);

        if (AreLocationsEqual(Location, Destination))
        {
            State = Shared.Enums.DroneState.Stop;
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
        var newLat = Location.Latitude + (Destination.Latitude - Location.Latitude) * stepSize;
        var newLon = Location.Longitude + (Destination.Longitude - Location.Longitude) * stepSize;
        return new Location
        {
            Latitude = newLat,
            Longitude = newLon
        };
    }
}
