using System.Text.Json.Serialization;
using Jetstream.Proxmox.Sample.WebAPI.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.MapGroup("/api/nodes")
    .MapNodesEndpoints();

app.Run();