using Jetstream.Proxmox.Sample.ProxmoxWrapper;
using Jetstream.Proxmox.Sample.ProxmoxWrapper.Base;
using Jetstream.Proxmox.Sample.WebAPI.Application.Common.Interfaces;
using Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Helpers;
using Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Persistence;
using Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Repositories;
using Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Services;

namespace Jetstream.Proxmox.Sample.WebAPI.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<DataContext>(_ => DataContext.CreateFromJson("sampleDatabase.json"));
        
        serviceCollection.AddScoped<IClustersRepository, ClustersRepository>();
        serviceCollection.AddProxmoxProvider(provider =>
            provider.GetRequiredService<IClustersRepository>().GetAll().ToProxmoxClustersList());
        serviceCollection.AddScoped<IProxmoxService, ProxmoxService>();
    }
}