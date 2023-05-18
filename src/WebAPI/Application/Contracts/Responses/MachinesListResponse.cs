using Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Dtos;

namespace Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Responses;

public class MachinesListResponse
{
    public IList<MachineBriefDto> Machines { get; set; }
}