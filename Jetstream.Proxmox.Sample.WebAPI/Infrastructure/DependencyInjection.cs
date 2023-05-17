using Jetstream.Proxmox.Sample.WebAPI.Application.Common.Interfaces;
using Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Persistence;
using Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Repositories;
using Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Services;
using Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Wrapper;

namespace Jetstream.Proxmox.Sample.WebAPI.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<DataContext>(_ => DataContext.CreateFromJson("sampleDatabase.json"));
        
        serviceCollection.AddScoped<IClustersRepository, ClustersRepository>();
        serviceCollection.AddScoped<IProxmoxClientProvider, ProxmoxClientProvider>(provider =>
        {
            var repository = provider.GetRequiredService<IClustersRepository>();
            var resources = repository.GetAll();

            return new ProxmoxClientProvider(resources.ToProxmoxClustersList());
        });
        serviceCollection.AddScoped<IProxmoxService, ProxmoxService>();
    }
}