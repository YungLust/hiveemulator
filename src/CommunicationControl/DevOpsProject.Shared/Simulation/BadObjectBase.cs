namespace DevOpsProject.Shared.Simulation;

public abstract record BadObjectBase(TimeSpan Latency, TimeSpan? Duration) : IDisposable
{
    public DateTimeOffset? StopTime { get; } = Duration.HasValue ? DateTimeOffset.UtcNow.Add(Duration.Value) : null;
    public bool IsActive => !Duration.HasValue || StopTime >= DateTimeOffset.UtcNow;
    public CancellationTokenSource CancellationTokenSource = new();
    public CancellationToken CancellationToken => CancellationTokenSource.Token;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            CancellationTokenSource?.Dispose();
            CancellationTokenSource = null;
        }
    }
}
