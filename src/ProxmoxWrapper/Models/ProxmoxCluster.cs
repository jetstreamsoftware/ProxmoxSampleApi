namespace Jetstream.Proxmox.Sample.ProxmoxWrapper.Models;

public class ProxmoxCluster
{
    public string Name { get; init; }
    public string Login { get; init; }
    public string Password { get; init; }
    public List<ProxmoxHost> Hosts { get; init; }
}
