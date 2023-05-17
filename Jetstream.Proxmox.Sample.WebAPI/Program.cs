using System.Text.Json.Serialization;
using Jetstream.Proxmox.Sample.WebAPI.Api.Endpoints;
using Jetstream.Proxmox.Sample.WebAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddInfrastructure();

var app = builder.Build();

app.MapGroup("/api/clusters")
    .MapClustersEndpoints();

app.Run();