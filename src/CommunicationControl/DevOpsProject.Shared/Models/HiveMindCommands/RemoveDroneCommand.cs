namespace DevOpsProject.Shared.Models.HiveMindCommands;

public sealed class RemoveDroneCommand : HiveMindCommand
{
    public string DroneId { get; set; }
}