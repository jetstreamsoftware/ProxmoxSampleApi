using Corsinvest.ProxmoxVE.Api.Shared.Models.Vm;
using Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Dtos;

namespace Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Responses;

public class SnapshotsListResponse
{
    public IList<SnapshotDto> Snapshots { get; set; }
}