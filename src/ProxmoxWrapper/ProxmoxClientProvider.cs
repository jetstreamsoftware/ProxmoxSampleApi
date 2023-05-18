using Corsinvest.ProxmoxVE.Api;
using Corsinvest.ProxmoxVE.Api.Extension;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Vm;
using Jetstream.Proxmox.Sample.ProxmoxWrapper.Base;
using Jetstream.Proxmox.Sample.ProxmoxWrapper.Exceptions;
using Jetstream.Proxmox.Sample.ProxmoxWrapper.Models;

using PveVmidItem = Corsinvest.ProxmoxVE.Api.PveClient.PveNodes.PveNodeItem.PveQemu.PveVmidItem;

namespace Jetstream.Proxmox.Sample.ProxmoxWrapper;

internal class ProxmoxClientProvider : IProxmoxClientProvider
{
    private readonly IList<ProxmoxCluster> _proxmoxClusters;

    public ProxmoxClientProvider(IList<ProxmoxCluster> proxmoxClusters)
    {
        _proxmoxClusters = proxmoxClusters;
    }

    public async Task<IEnumerable<NodeVmQemu>> GetMachinesList(string clusterName, string hostName)
    {
        // Try creating client from the list above
        var client = await CreateClient(clusterName, hostName);
        // No active hosts in the cluster
        if (client is null)
            throw new NoActiveHostsException(clusterName);
        
        // Get and return machines list
        var machines = await client.Nodes[hostName].Qemu.Get();
        return machines ?? throw new InternalProxmoxException();
    }

    public async Task<IEnumerable<VmSnapshot>> GetMachineSnapshots(string clusterName, string hostName, int machineId)
    {
        var machine = await GetMachine(clusterName, hostName, machineId);
        var list = await machine.Snapshot.Get();
        
        return list ?? throw new InternalProxmoxException();
    }
    
    public async Task<VmQemuStatusCurrent> GetMachineStatus(string clusterName, string hostName, int machineId)
    {
        var machine = await GetMachine(clusterName, hostName, machineId);
        var status = await machine.Status.Current.Get();
        
        return status ?? throw new InternalProxmoxException();
    }

    private async Task<PveVmidItem> GetMachine(string clusterName, string hostName, int machineId)
    {
        // Order clusters so that the provided cluster is the first one
        foreach (var cluster in _proxmoxClusters.OrderByDescending(clstr => clstr.Name == clusterName).ThenBy(clstr => clstr))
        {
            // 1. Try create client using specified host
            var client = await CreateClient(cluster.Name, hostName);
            
            // 2. When we cannot connect to any host in the cluster -> try with another cluster
            if (client is null)
                continue;

            // 3. Check if machine exists in the specified host
            var result = await client.Cluster.Resources.Get();
            
            
            if (result is null)
                continue;

        }
    }

    private async Task<PveClient?> CreateClient(string clusterName, string hostName)
    {
        var cluster = _proxmoxClusters.FirstOrDefault(cluster => cluster.Name == clusterName);
        if (cluster is null)
            throw new ResourceNotFoundException(nameof(ProxmoxCluster), clusterName);
        
        if (cluster.Hosts.All(host => host.Name != hostName))
            throw new ResourceNotFoundException(nameof(ProxmoxHost), hostName);
        
        // Order hosts so that the first one is specified host, but in case there is any error, try login with another hosts
        foreach (var proxmoxHost in cluster.Hosts.OrderByDescending(hst => hst.Name == hostName).ThenBy(hst => hst))
        {
            var client = new PveClient(proxmoxHost.Address);

            if (await client.Login(cluster.Login, cluster.Password))
                return client;
        }

        return null;
    }
}
