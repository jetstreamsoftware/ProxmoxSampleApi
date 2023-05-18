namespace Jetstream.Proxmox.Sample.ProxmoxWrapper.Base;

public interface IProxmoxClientProvider
{
    Task<dynamic> GetMachinesList(string clusterName, string hostName);
    Task<dynamic> GetMachineSnapshots(string clusterName, string hostName, int machineId);
    Task<dynamic> GetMachineStatus(string clusterName, string hostName, int machineId);
}