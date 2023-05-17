using Jetstream.Proxmox.Sample.WebAPI.Application.Common.Interfaces;
using Jetstream.Proxmox.Sample.WebAPI.Application.Models;
using Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Persistence;

namespace Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Repositories;

public class ClustersRepository : IClustersRepository
{
    private readonly DataContext _dataContext;

    public ClustersRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public IEnumerable<Cluster> GetAll()
        => _dataContext.Clusters.ToList();

    public Cluster? GetByName(string name)
        => _dataContext.Clusters.FirstOrDefault(x => x.Name == name);
}