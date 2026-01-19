namespace DevOpsProject.Shared.Simulation;

public interface ISimulationUtility
{
    BadDeviceDto BadDevice { get; }
    void SimulateBadDevice(BadDeviceDto badDevice);
    void StopBadDeviceSimulation();
    bool StopBadConnectionSimulation(string connectionName);
    BadConnectionDto GetBadConnection(string connectionName);
    void SimulateBadConnection(BadConnectionDto badConnection);
}
