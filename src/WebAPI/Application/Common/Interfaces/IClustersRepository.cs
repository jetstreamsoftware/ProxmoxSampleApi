using Jetstream.Proxmox.Sample.WebAPI.Application.Models;

namespace Jetstream.Proxmox.Sample.WebAPI.Application.Common.Interfaces;

public interface IClustersRepository
{
    IEnumerable<Cluster> GetAll();
    Cluster? GetByName(string name);
}