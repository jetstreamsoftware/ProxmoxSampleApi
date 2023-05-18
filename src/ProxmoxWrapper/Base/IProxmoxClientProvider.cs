using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Vm;

namespace Jetstream.Proxmox.Sample.ProxmoxWrapper.Base;

public interface IProxmoxClientProvider
{
    Task<IEnumerable<NodeVmQemu>> GetMachinesList(string clusterName, string hostName);
    Task<IEnumerable<VmSnapshot>> GetMachineSnapshots(string clusterName, string hostName, int machineId);
    Task<VmQemuStatusCurrent> GetMachineStatus(string clusterName, string hostName, int machineId);
}