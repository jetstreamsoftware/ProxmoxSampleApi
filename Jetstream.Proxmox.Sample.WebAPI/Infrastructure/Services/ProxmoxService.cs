using FluentResults;
using Jetstream.Proxmox.Sample.WebAPI.Application.Common.Interfaces;
using Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Dtos;
using Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Responses;
using Result = FluentResults.Result;

namespace Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Services;

public class ProxmoxService : IProxmoxService
{
    private readonly IProxmoxClientProvider _clientProvider;

    public ProxmoxService(IProxmoxClientProvider clientProvider)
    {
        _clientProvider = clientProvider;
    }

    public async Task<Result<MachinesListResponse>> GetMachinesList(string clusterName, string hostName)
        => await Result.Try(() => _GetMachinesList(clusterName, hostName));

    public async Task<Result<SnapshotsListResponse>> GetSnapshotsList(string clusterName, string hostName, int machineId)
        => await Result.Try(() => _GetSnapshotsList(clusterName, hostName, machineId));

    public async Task<Result<MachineStatusDto>> GetResourcesUtilization(string clusterName, string hostName, int machineId)
        => await Result.Try(() => _GetResourcesUtilization(clusterName, hostName, machineId));

    private async Task<MachinesListResponse> _GetMachinesList(string clusterName, string hostName)
    {
        var response = await _clientProvider.GetMachinesList(clusterName, hostName);
        return (MachinesListResponse)response;
    }

    private async Task<SnapshotsListResponse> _GetSnapshotsList(string clusterName, string hostName, int machineId)
    {
        var response = await _clientProvider.GetMachineSnapshots(clusterName, hostName, machineId);
        return (SnapshotsListResponse)response;
    }

    private async Task<MachineStatusDto> _GetResourcesUtilization(string clusterName, string hostName, int machineId)
    {
        var response = await _clientProvider.GetMachineStatus(clusterName, hostName, machineId);
        return (MachineStatusDto)response;
    }
}