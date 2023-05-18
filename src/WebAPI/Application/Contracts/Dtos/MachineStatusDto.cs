using Corsinvest.ProxmoxVE.Api.Shared.Models.Vm;

namespace Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Dtos;

public class MachineStatusDto
{
    public string RunningMachine { get; set; }
    public string QmpStatus { get; set; }
    public bool Agent { get; set; }
    public long Balloon { get; set; }

    public static implicit operator MachineStatusDto(VmQemuStatusCurrent status)
        => new()
        {
            RunningMachine = status.RunningMachine,
            QmpStatus = status.Qmpstatus,
            Agent = status.Agent,
            Balloon = status.Balloon
        };
}