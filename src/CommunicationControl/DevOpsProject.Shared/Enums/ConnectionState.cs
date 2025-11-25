namespace DevOpsProject.Shared.Enums;

public enum ConnectionState
{
    Undefined,
    Alive,
    Dead,
    DeadNonRecoverable      // Used only for simulation and testing.
}
