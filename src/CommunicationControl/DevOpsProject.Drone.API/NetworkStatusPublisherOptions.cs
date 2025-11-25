using System.ComponentModel.DataAnnotations;

namespace DevOpsProject.Drone.API;

public class NetworkStatusPublisherOptions
{
    [Range(1, int.MaxValue)]
    public int DelayInMilliseconds { get; set; }
}
