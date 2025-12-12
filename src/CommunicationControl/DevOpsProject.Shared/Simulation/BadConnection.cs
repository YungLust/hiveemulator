namespace DevOpsProject.Shared.Simulation;

public sealed record BadConnection(string Name, TimeSpan Latency, TimeSpan? Duration) : BadObjectBase(Latency, Duration)
{
    public static BadConnection FromDto(BadConnectionDto dto)
    {
        return new BadConnection(dto.Name, dto.Latency, dto.Duration);
    }

    public BadConnectionDto ToDto()
    {
        return new BadConnectionDto(Name, Latency, Duration)
        {
            CancellationToken = CancellationToken
        };
    }
}
