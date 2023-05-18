using FluentResults;
using Jetstream.Proxmox.Sample.ProxmoxWrapper.Base;
using Jetstream.Proxmox.Sample.WebAPI.Application.Common.Exceptions;
using Jetstream.Proxmox.Sample.WebAPI.Application.Common.Interfaces;
using Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Dtos;
using Jetstream.Proxmox.Sample.WebAPI.Application.Contracts.Responses;
using Jetstream.Proxmox.Sample.WebAPI.Application.Models;
using Host = Jetstream.Proxmox.Sample.WebAPI.Application.Models.Host;
using Result = FluentResults.Result;

namespace Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Services;

public class ProxmoxService : IProxmoxService
{
    private readonly IClustersRepository _clustersRepository;
    private readonly IProxmoxClientProvider _clientProvider;

    public ProxmoxService(IProxmoxClientProvider clientProvider, IClustersRepository clustersRepository)
    {
        _clientProvider = clientProvider;
        _clustersRepository = clustersRepository;
    }

    public async Task<Result<MachinesListResponse>> GetMachinesList(string clusterName, string hostName)
        => await Result.Try(() => _GetMachinesList(clusterName, hostName));

    public async Task<Result<SnapshotsListResponse>> GetSnapshotsList(int machineId)
        => await Result.Try(() => _GetSnapshotsList(machineId));

    public async Task<Result<MachineStatusDto>> GetResourcesUtilization(int machineId)
        => await Result.Try(() => _GetResourcesUtilization(machineId));

    private async Task<MachinesListResponse> _GetMachinesList(string clusterName, string hostName)
    {
        var response = await _clientProvider.GetMachinesList(clusterName, hostName);
        return (MachinesListResponse)response;
    }

    private async Task<SnapshotsListResponse> _GetSnapshotsList(int machineId)
    {
        var (cluster, host) = ResolveMachineHost(machineId);
            
        var response = await _clientProvider.GetMachineSnapshots(cluster.Name, host.Name, machineId);
        return (SnapshotsListResponse)response;
    }

    private async Task<MachineStatusDto> _GetResourcesUtilization(int machineId)
    {
        var (cluster, host) = ResolveMachineHost(machineId);
        
        var response = await _clientProvider.GetMachineStatus(cluster.Name, host.Name, machineId);
        return (MachineStatusDto)response;
    }

    private (Cluster, Host) ResolveMachineHost(int machineId)
    {
        var cluster = _clustersRepository.GetAll().FirstOrDefault(cluster =>
            cluster.Hosts.Any(host => host.Machines.Any(machine => machine.MachineId == machineId.ToString())));
        if (cluster is null)
            throw new EntityNotFoundException(nameof(Machine), machineId);

        var host = cluster.Hosts.FirstOrDefault(hst =>
            hst.Machines.Any(machine => machine.MachineId == machineId.ToString()));

        return (cluster, host!);
    }
}