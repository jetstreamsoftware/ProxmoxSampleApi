namespace Jetstream.Proxmox.Sample.WebAPI.Application.Common.Interfaces;

public interface IProxmoxService
{
    Task MachinesList(string nodeName, string hostName);
    Task SnapshotsList(string nodeName, string hostName, int machineId);
    Task ResourcesUtilization(string nodeName, string hostName, int machineId);
}