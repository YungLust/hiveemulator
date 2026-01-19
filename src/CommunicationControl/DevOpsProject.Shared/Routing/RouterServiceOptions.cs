using System.ComponentModel.DataAnnotations;
using DevOpsProject.Shared.Models;

namespace DevOpsProject.Shared.Routing;

public class RouterServiceOptions
{
    [Required]
    public TimeSpan RouterUpdaterDelay { get; set; }
    [Required]
    public Connection CurrentConnection { get; set; }
    [Required]
    public TimeSpan AdditionalLateDelay { get; set; }
}