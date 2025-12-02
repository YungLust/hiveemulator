using System.ComponentModel.DataAnnotations;

namespace DevOpsProject.HiveMind.API;

public class NetworkStatusPublisherOptions
{
    [Required]
    public TimeSpan Delay { get; set; }
}
