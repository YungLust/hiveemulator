namespace DevOpsProject.HiveMind.Logic.Services.Interfaces;

public interface ISimulationService
{
    bool AddIgnoredConnection(string connectionName, TimeSpan? duration = null);
    bool RemoveIgnoredConnection(string connectionName);
    bool IsIgnoredConnection(string connectionName);
}