using Jetstream.Proxmox.Sample.WebAPI.Application.Common.Interfaces;

namespace Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Services;

public class ProxmoxService
{
    private readonly INodesRepository _nodesRepository;

    public ProxmoxService(INodesRepository nodesRepository)
    {
        _nodesRepository = nodesRepository;
    }
}