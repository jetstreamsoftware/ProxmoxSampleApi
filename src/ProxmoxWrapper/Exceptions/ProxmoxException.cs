namespace Jetstream.Proxmox.Sample.ProxmoxWrapper.Exceptions;

public class ProxmoxException : Exception
{
    public ProxmoxException(string message)
        : base(message)
    {
        
    }
}