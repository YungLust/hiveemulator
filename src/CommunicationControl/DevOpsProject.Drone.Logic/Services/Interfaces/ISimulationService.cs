namespace DevOpsProject.Drone.Logic.Services.Interfaces;

public interface ISimulationService
{
    bool IsStopped { get; }
    void Stop(TimeSpan? duration = null);
    void Restart();
    bool RemoveIgnoredConnection(string connectionName);
    bool IsIgnoredConnection(string connectionName);
    bool AddIgnoredConnection(string connectionName, TimeSpan? duration = null);
}
