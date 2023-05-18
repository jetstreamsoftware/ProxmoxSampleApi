using Corsinvest.ProxmoxVE.Api.Shared.Models.Vm;

namespace Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Dtos;

public class SnapshotDto
{
    public string Parent { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateTime { get; set; }

    public static implicit operator SnapshotDto(VmSnapshot snapshot)
    {
        return new SnapshotDto
        {
            Parent = snapshot.Parent,
            Name = snapshot.Name,
            Description = snapshot.Description,
            DateTime = snapshot.Date
        };
    }
}