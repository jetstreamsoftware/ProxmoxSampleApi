using Microsoft.AspNetCore.Mvc;

namespace Jetstream.Proxmox.Sample.WebAPI.Api.Endpoints;

public static class NodesEndpoints
{
    public static RouteGroupBuilder MapNodesEndpoints(this RouteGroupBuilder builder)
    {
        // Get list of machines the specified from host (method 1.)
        builder.MapGet("/{nodeName}/hosts/{hostName}/machines",
            ([FromRoute] string nodeName, [FromRoute] string hostName) =>
            {
                return Results.Json(new { Name = $"{nodeName}", Host = $"{hostName}" });
            });

        // Get resources information for the specified node, host and machine id (method 3.)
        builder.MapGet("/{nodeName}/hosts/{hostName}/machines/{machineId:int}", 
            ([FromRoute] string nodeName, [FromRoute] string hostName, [FromRoute] int machineId) =>
            {
                return Results.Json(new { Name = $"{nodeName}", Host = $"{hostName}", MachineId = machineId });
            });
        
        // Get list of snapshots for the specified node, host and machine id (method 2.)
        builder.MapGet("/{nodeName}/hosts/{hostName}/machines/{machineId:int}/snapshots",
            ([FromRoute] string nodeName, [FromRoute] string hostName, [FromRoute] int machineId) =>
            {
                return Results.Json(new { Name = "2" });
            });

        return builder;
    }
}