using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;

namespace Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Dtos;

public class MachineBriefDto
{
    public string Status { get; set; }
    public long Uptime { get; set; }
    public long DiskSize { get; set; }
    public long MemorySize { get; set; }

    public static implicit operator MachineBriefDto(NodeVmQemu qemu)
        => new()
        {
            Status = qemu.Status,
            Uptime = qemu.Uptime,
            DiskSize = qemu.DiskSize,
            MemorySize = qemu.MemorySize
        };
}