namespace DevOpsProject.Shared.Simulation;

public sealed record BadDevice(TimeSpan Latency, TimeSpan? Duration) : BadObjectBase(Latency, Duration)
{
    public static BadDevice FromDto(BadDeviceDto dto)
    {
        return new BadDevice(dto.Latency, dto.Duration);
    }

    public BadDeviceDto ToDto()
    {
        return new BadDeviceDto(Latency, Duration)
        {
            CancellationToken = CancellationToken
        };
    }
}
