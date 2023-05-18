namespace Jetstream.Proxmox.Sample.ProxmoxWrapper.Exceptions;

public class InternalProxmoxException : ProxmoxException
{
    public InternalProxmoxException()
        : base("There was an error in the Proxmox response")
    {
        
    }

    public InternalProxmoxException(string message)
        : base(message)
    {
        
    }
}