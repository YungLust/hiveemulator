using DevOpsProject.HiveMind.Logic.Services.Interfaces;

namespace DevOpsProject.HiveMind.Logic.Services;

public sealed class SimulationService : ISimulationService
{
    public IDictionary<string, DateTimeOffset?> IgnoredConnectionNames {get;} = new Dictionary<string, DateTimeOffset?>();
    
    public bool AddIgnoredConnection(string connectionName, TimeSpan? duration)
    {
        IgnoredConnectionNames[connectionName] = !duration.HasValue ? null : DateTimeOffset.UtcNow.Add(duration.Value);
        return true;
    }

    public bool RemoveIgnoredConnection(string connectionName)
    {
        return IgnoredConnectionNames.Remove(connectionName);
    }

    public bool IsIgnoredConnection(string connectionName)
    {
        var containsName = IgnoredConnectionNames.TryGetValue(connectionName, out var ignoredConnection);
        return containsName && (!ignoredConnection.HasValue || ignoredConnection.Value >= DateTimeOffset.UtcNow);
    }
}