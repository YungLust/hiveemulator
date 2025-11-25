using System.ComponentModel.DataAnnotations;
using DevOpsProject.Shared.Models;

namespace DevOpsProject.Drone.Logic.State;

public class DroneInitialStateOptions
{
    [Required]
    public string DroneId { get; set; }
    public Location Location { get; set; }
}
