using Jetstream.Proxmox.Sample.WebAPI.Api.Wrapper;
using Jetstream.Proxmox.Sample.WebAPI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jetstream.Proxmox.Sample.WebAPI.Api.Endpoints;

public static class ClustersEndpoints
{
    public static RouteGroupBuilder MapClustersEndpoints(this RouteGroupBuilder builder)
    {
        // Get list of machines the specified from host (method 1.)
        builder.MapGet("/{clusterName}/hosts/{hostName}/machines",
            async ([FromRoute] string clusterName, [FromRoute] string hostName, IProxmoxService proxmoxService) =>
            {
                var response = await proxmoxService.GetMachinesList(clusterName, hostName);
                return Results.Json(response.ToApiResult());
            });

        return builder;
    }
}