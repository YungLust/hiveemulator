namespace DevOpsProject.Shared.Simulation;

public sealed record BadConnectionDto(string Name, TimeSpan Latency, TimeSpan? Duration)
{
    public CancellationToken CancellationToken { get; set; } = CancellationToken.None;
}