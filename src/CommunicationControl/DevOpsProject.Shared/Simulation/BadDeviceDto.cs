namespace DevOpsProject.Shared.Simulation;

public sealed record BadDeviceDto(TimeSpan Latency, TimeSpan? Duration)
{
    public CancellationToken CancellationToken { get; set; } = CancellationToken.None;
}
