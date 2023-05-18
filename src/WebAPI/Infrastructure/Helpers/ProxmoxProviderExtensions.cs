using Jetstream.Proxmox.Sample.ProxmoxWrapper.Models;
using Jetstream.Proxmox.Sample.WebAPI.Application.Models;

namespace Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Helpers;

public static class ProxmoxProviderExtensions
{
    public static IList<ProxmoxCluster> ToProxmoxClustersList(this IEnumerable<Cluster> clusters)
    {
        return clusters.Select(cluster => new ProxmoxCluster
        {
            Name = cluster.Name,
            Login = cluster.Login,
            Password = cluster.Password,
            Hosts = cluster.Hosts.Select(host => new ProxmoxHost
            {
                Name = host.Name,
                Address = host.Address,
                Port = host.Port
            }).ToList()
        }).ToList();
    }
}