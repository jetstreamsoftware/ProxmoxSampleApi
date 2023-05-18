using Jetstream.Proxmox.Sample.WebAPI.Application.Models;

namespace Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Persistence;

public class Database
{
    public IList<Cluster> Clusters { get; set; }
}