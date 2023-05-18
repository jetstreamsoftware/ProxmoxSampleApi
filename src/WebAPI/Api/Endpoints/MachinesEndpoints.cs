using Jetstream.Proxmox.Sample.WebAPI.Api.Wrapper;
using Jetstream.Proxmox.Sample.WebAPI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jetstream.Proxmox.Sample.WebAPI.Api.Endpoints;

public static class MachinesEndpoints
{
    public static RouteGroupBuilder MapMachinesEndpoints(this RouteGroupBuilder builder)
    {
        // Get status information for the specified node, host and machine id (method 3.)
        builder.MapGet("/{machineId:int}/status",
            async ([FromRoute] int machineId, IProxmoxService proxmoxService) =>
            {
                var response = await proxmoxService.GetResourcesUtilization(machineId);
                return Results.Json(response.ToApiResult());
            });
        
        // Get list of snapshots for the specified node, host and machine id (method 2.)
        builder.MapGet("/{machineId:int}/snapshots",
            async ([FromRoute] int machineId, IProxmoxService proxmoxService) =>
            {
                var response = await proxmoxService.GetSnapshotsList(machineId);
                return Results.Json(response.ToApiResult());
            });

        return builder;
    }
}