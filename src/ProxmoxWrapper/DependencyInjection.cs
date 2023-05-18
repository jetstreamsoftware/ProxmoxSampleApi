using Jetstream.Proxmox.Sample.ProxmoxWrapper.Base;
using Jetstream.Proxmox.Sample.ProxmoxWrapper.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Jetstream.Proxmox.Sample.ProxmoxWrapper;

public static class DependencyInjection
{
    public static IServiceCollection AddProxmoxProvider(this IServiceCollection serviceCollection,
        IEnumerable<ProxmoxCluster> dataSource)
    {
        serviceCollection.AddScoped<IProxmoxClientProvider, ProxmoxClientProvider>(_ =>
            new ProxmoxClientProvider(dataSource.ToList()));

        return serviceCollection;
    }
    
    public static IServiceCollection AddProxmoxProvider(this IServiceCollection serviceCollection,
        Func<IServiceProvider, IEnumerable<ProxmoxCluster>> dynamicDataSource)
    {
        serviceCollection.AddScoped<IProxmoxClientProvider, ProxmoxClientProvider>(provider =>
            new ProxmoxClientProvider(dynamicDataSource.Invoke(provider).ToList()));
        
        return serviceCollection;
    }
}