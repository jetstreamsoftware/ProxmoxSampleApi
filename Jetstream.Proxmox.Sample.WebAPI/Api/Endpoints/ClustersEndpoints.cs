using Jetstream.Proxmox.Sample.WebAPI.Api.Wrapper;
using Jetstream.Proxmox.Sample.WebAPI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jetstream.Proxmox.Sample.WebAPI.Api.Endpoints;

public static class ClustersEndpoints
{
    public static RouteGroupBuilder MapClustersEndpoints(this RouteGroupBuilder builder)
    {
        // Get list of machines the specified from host (method 1.)
        builder.MapGet("/{nodeName}/hosts/{hostName}/machines",
            async ([FromRoute] string nodeName, [FromRoute] string hostName, IProxmoxService proxmoxService) =>
            {
                var response = await proxmoxService.GetMachinesList(nodeName, hostName);
                return Results.Json(response.ToApiResult());
            });

        // Get resources information for the specified node, host and machine id (method 3.)
        builder.MapGet("/{nodeName}/hosts/{hostName}/machines/{machineId:int}",
            async ([FromRoute] string nodeName, [FromRoute] string hostName, [FromRoute] int machineId,
                IProxmoxService proxmoxService) =>
            {
                var response = await proxmoxService.GetResourcesUtilization(nodeName, hostName, machineId);
                return Results.Json(response.ToApiResult());
            });
        
        // Get list of snapshots for the specified node, host and machine id (method 2.)
        builder.MapGet("/{nodeName}/hosts/{hostName}/machines/{machineId:int}/snapshots",
            async ([FromRoute] string nodeName, [FromRoute] string hostName, [FromRoute] int machineId,
                IProxmoxService proxmoxService) =>
            {
                var response = await proxmoxService.GetSnapshotsList(nodeName, hostName, machineId);
                return Results.Json(response.ToApiResult());
            });

        return builder;
    }
}