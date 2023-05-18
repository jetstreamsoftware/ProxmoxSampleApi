using FluentResults;
using Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Dtos;
using Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Responses;

namespace Jetstream.Proxmox.Sample.WebAPI.Application.Common.Interfaces;

public interface IProxmoxService
{
    Task<Result<MachinesListResponse>> GetMachinesList(string clusterName, string hostName);
    Task<Result<SnapshotsListResponse>> GetSnapshotsList(int machineId);
    Task<Result<MachineStatusDto>> GetResourcesUtilization(int machineId);
}