namespace Jetstream.Proxmox.Sample.WebAPI.Application.Common.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message)
        : base(message)
    {
        
    }
    
    public EntityNotFoundException(string name, object identifier)
        : base($"Entity of type '{name}' with identifier #{identifier} was not found.")
    {
        
    }
}