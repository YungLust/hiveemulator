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
        Type = options.Value.Type;
    }
    
    private readonly Lock _movementLock = new();
    
    public string Name => Connection.GetName(DroneId, ConnectionType.Drone);
    public string DroneId { get; }

    public Location Location
    {
        get
        {
            lock (_movementLock)
            {
                return field;
            }
        }
        set
        {
            lock (_movementLock)
            {
                field = value;
            }
        }
    }
    
    public DroneType Type { get; }

    public Location? Destination
    {
        get
        {
            lock (_movementLock)
            {
                return field;
            }
        }
        set
        {
            lock (_movementLock)
            {
                field = value;
            }
        }
    }

    public float Speed { get; set; }
    public float Height { get; set; }

    public DevOpsProject.Shared.Enums.DroneState State
    {
        get
        {
            lock (_movementLock)
            {
                return field;
            }
        }
        set
        {
            lock (_movementLock)
            {
                field = value;
            }
        }
    }
}
