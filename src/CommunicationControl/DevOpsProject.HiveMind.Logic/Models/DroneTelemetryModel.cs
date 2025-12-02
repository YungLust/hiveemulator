using DevOpsProject.Shared.Enums;
using DevOpsProject.Shared.Models;

namespace DevOpsProject.HiveMind.Logic.Models;

public sealed record DroneTelemetryModel
{
    public string Id { get; set; }
    public Location Location { get; set; }
    public float Speed { get; set; }
    public float Height { get; set; }
    public DroneType DroneType { get; set; }
    public DateTimeOffset LastUpdatedAt { get; set; }
    public DroneState State { get; set; }
    public Location? Destination { get; set; }
}
