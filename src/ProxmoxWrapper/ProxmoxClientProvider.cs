using Corsinvest.ProxmoxVE.Api;
using Jetstream.Proxmox.Sample.ProxmoxWrapper.Base;
using Jetstream.Proxmox.Sample.ProxmoxWrapper.Exceptions;
using Jetstream.Proxmox.Sample.ProxmoxWrapper.Models;

using PveVmidItem = Corsinvest.ProxmoxVE.Api.PveClient.PveNodes.PveNodeItem.PveQemu.PveVmidItem;

namespace Jetstream.Proxmox.Sample.ProxmoxWrapper;

public class ProxmoxClientProvider : IProxmoxClientProvider
{
    private readonly IList<ProxmoxCluster> _proxmoxClusters;

    public ProxmoxClientProvider(IList<ProxmoxCluster> proxmoxClusters)
    {
        _proxmoxClusters = proxmoxClusters;
    }

    public async Task<dynamic> GetMachinesList(string clusterName, string hostName)
    {
        var cluster = _proxmoxClusters.FirstOrDefault(cluster => cluster.Name == clusterName);
        if (cluster is null)
            throw new ResourceNotFoundException(nameof(ProxmoxCluster), clusterName);

        if (cluster.Hosts.All(host => host.Name != hostName))
            throw new ResourceNotFoundException(nameof(ProxmoxHost), hostName);

        var host = cluster.Hosts.FirstOrDefault(hst => hst.Name == hostName);

        // Try creating client from the list above
        var client = await CreateClient(cluster, host!);
        // No active hosts in the cluster
        if (client is null)
            throw new NoActiveHostsException(clusterName);
        
        // Get and return machines list
        var vm = await client.Nodes[hostName].Qemu.Vmlist();
        return vm.Response;
    }

    public async Task<dynamic> GetMachineSnapshots(string clusterName, string hostName, int machineId)
    {
        var machine = await GetMachine(clusterName, hostName, machineId);
        var list = await machine.Snapshot.SnapshotList();
        
        return list.IsSuccessStatusCode ? list.Response : throw new InternalProxmoxException(list.GetError());
    }
    
    public async Task<dynamic> GetMachineStatus(string clusterName, string hostName, int machineId)
    {
        var machine = await GetMachine(clusterName, hostName, machineId);
        var status = await machine.Status.Current.VmStatus();
        
        return status.IsSuccessStatusCode ? status.Response : throw new InternalProxmoxException(status.GetError());
    }

    private async Task<PveVmidItem> GetMachine(string clusterName, string hostName, int machineId)
    {
        throw new NotImplementedException();
    }

    private static async Task<PveClient?> CreateClient(ProxmoxCluster cluster, ProxmoxHost host)
    {
        // Order hosts so that the first one is specified host, but in case there is any error, try login with another hosts
        foreach (var proxmoxHost in cluster.Hosts.OrderByDescending(hst => hst.Name == host.Name).ThenBy(hst => hst))
        {
            var client = new PveClient(proxmoxHost.Address);

            if (await client.Login(cluster.Login, cluster.Password))
                return client;
        }

        return null;
    }
}
