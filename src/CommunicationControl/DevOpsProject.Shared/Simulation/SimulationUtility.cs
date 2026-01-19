using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DevOpsProject.Shared.Simulation;

public sealed class SimulationUtility : ISimulationUtility
{
    public BadDeviceDto BadDevice => _badDevice is { IsActive: true }
        ? _badDevice.ToDto()
        : null;
    private BadDevice _badDevice;
    
    private readonly ConcurrentDictionary<string, BadConnection> _connections = new();
    
    public void SimulateBadDevice(BadDeviceDto badDevice)
    {
        if (_badDevice is not null)
        {
            StopBadDeviceSimulation();
        }
        
        _badDevice = DevOpsProject.Shared.Simulation.BadDevice.FromDto(badDevice);
    }

    public void StopBadDeviceSimulation()
    {
        if (_badDevice is null)
        {
            return;
        }
        
        _badDevice.CancellationTokenSource.Cancel();
        _badDevice.Dispose();
        _badDevice = null;
    }

    public bool StopBadConnectionSimulation(string connectionName)
    {
        var result = _connections.TryRemove(connectionName, out var connection);
        if (result)
        {
            connection.CancellationTokenSource.Cancel();
            connection.Dispose();
        }
        
        return result;
    }

    public BadConnectionDto GetBadConnection(string connectionName)
    {
        var value = _connections.GetValueOrDefault(connectionName);
        
        return value is { IsActive: true } ? value.ToDto() : null;
    }

    public void SimulateBadConnection(BadConnectionDto badConnection)
    {
        var badConnectionModel = BadConnection.FromDto(badConnection);
        _connections.AddOrUpdate(
            badConnectionModel.Name,
            _ => badConnectionModel,
            (_, old) =>
            {
                old.CancellationTokenSource.Cancel();
                old.Dispose();
                return badConnectionModel;
            });
    }
}
