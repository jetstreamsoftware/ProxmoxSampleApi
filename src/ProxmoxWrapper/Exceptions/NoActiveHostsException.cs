namespace Jetstream.Proxmox.Sample.ProxmoxWrapper.Exceptions;

public class NoActiveHostsException : ProxmoxException
{
    public NoActiveHostsException(string clusterName)
        : base($"No active hosts found in the cluster '{clusterName}'")
    {
        
    }
}