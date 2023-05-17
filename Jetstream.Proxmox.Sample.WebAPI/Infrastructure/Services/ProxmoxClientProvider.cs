using Corsinvest.ProxmoxVE.Api;
using Jetstream.Proxmox.Sample.WebAPI.Application.Common.Interfaces;
using PveVmidItem = Corsinvest.ProxmoxVE.Api.PveClient.PveNodes.PveNodeItem.PveQemu.PveVmidItem;

namespace Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Services;

// Wrapper library for PveClient providing additional logic and mechanisms
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

public class ProxmoxCluster
{
    public string Name { get; init; }
    public string Login { get; init; }
    public string Password { get; init; }
    public List<ProxmoxHost> Hosts { get; init; }
}

public class ProxmoxHost
{
    public string Name { get; init; }
    public string Address { get; init; }
    public string Port { get; init; }
}

public class ProxmoxException : Exception
{
    public ProxmoxException(string message)
        : base(message)
    {
        
    }
}

public class InternalProxmoxException : ProxmoxException
{
    public InternalProxmoxException()
        : base("There was an error in the Proxmox response")
    {
        
    }

    public InternalProxmoxException(string message)
        : base(message)
    {
        
    }
}

public class ResourceNotFoundException : ProxmoxException
{
    public ResourceNotFoundException(string type, object identifier)
        : base($"The resource of type '{type}' with identifier '{identifier}' cannot be found in the config.")
    {
        
    }
}

public class NoActiveHostsException : ProxmoxException
{
    public NoActiveHostsException(string clusterName)
        : base($"No active hosts found in the cluster '{clusterName}'")
    {
        
    }
}
