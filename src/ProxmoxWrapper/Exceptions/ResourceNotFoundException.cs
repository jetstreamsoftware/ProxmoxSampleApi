namespace Jetstream.Proxmox.Sample.ProxmoxWrapper.Exceptions;

public class ResourceNotFoundException : ProxmoxException
{
    public ResourceNotFoundException(string type, object identifier)
        : base($"The resource of type '{type}' with identifier '{identifier}' cannot be found in the config.")
    {
        
    }
}