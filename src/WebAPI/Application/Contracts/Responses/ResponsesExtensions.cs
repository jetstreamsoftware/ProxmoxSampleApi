using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Vm;
using Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Dtos;

namespace Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Responses;

public static class ResponsesExtensions
{
    public static MachinesListResponse ToMachinesListResponse(this IEnumerable<NodeVmQemu> qemus)
        => new() { Machines = qemus.Select(qemu => (MachineBriefDto)qemu).ToList() };

    public static SnapshotsListResponse ToSnapshotsListResponse(this IEnumerable<VmSnapshot> snapshots)
        => new() { Snapshots = snapshots.Select(snapshot => (SnapshotDto)snapshot).ToList() };
}