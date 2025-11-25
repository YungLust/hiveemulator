using System.ComponentModel.DataAnnotations;

namespace DevOpsProject.Shared.Routing;

public class RouterServiceOptions
{
    [Range(100, int.MaxValue)]
    public int RouterUpdatedDelayInMilliseconds { get; set; }
    [Range(100, int.MaxValue)]
    public int IsAliveCheckerDelayInMilliseconds { get; set; }
    [Range(100, int.MaxValue)]
    public int IsAliveCheckerMaxDifferenceInMilliseconds { get; set; }
    [Required]
    public Func<string> CurrentConnectionNameProvider { get; set; }
}